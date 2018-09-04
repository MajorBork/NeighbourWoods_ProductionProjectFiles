using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using Manager.UI;
using Manager.Inventory;
using Manager;
using TMPro;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using Language.Lua;
namespace Manager.Player
{
    #region Vision Enum
    public enum Vision //The Enum that controls which vision state you are in  
    {
        NORMAL,
        SMELL,
    }
    #endregion
    #region PlayerManager Class
    public class PlayerManager : MonoBehaviour //Singleton<PlayerManager> // An script that will not be destroyed when going new levels (if we need to load new levels)
    {
        #region Variables
        public Vision vision;
        public GameState gameState;
        //public TMP_Text testTMP;
        #region Object Variables
        // All of the Variables that link to objects or components
        public GameObject player;
        public UIManager gameManagerUI;
        public GameManager gameManager;
        public GameObject playerCamera;
        public Transform playerCameraTransform;
        public PostProcessingBehaviour smellOVision;
        public CharacterController characterController;
        public GameObject[] smellObjects = new GameObject[0];
        #endregion
        #region Control Variables
        //Space//
        //All of the controls variables used in the movement functions
        public float mouseSensitivity = 10;
        public float walkSpeed = 2;
        public float runSpeed = 6;
        public float gravity = -12;
        public float jumpHeight = 1;
        [Range(0, 1)]
        public float airControlPercent;
        private float velocityY;
        public float turnSmoothTime = 0.2f;
        public float speedSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        private float speedSmoothVelocity;
        private float currentSpeed;
        public bool lockCursor;
        Vector3 currentRotation;
        Vector3 rotationSmoothVelocity;
        public Vector2 pitchMinMax = new Vector2(-40,85);
        public float cameraRotation;
        public Transform target;
        private float yaw = 0;
        private float pitch = 0;
        #endregion
        #region Vision Variables
        [SerializeField]
        public float maxVignIntensity = 0.3f;
        [SerializeField]
        public float vignDuration = 0.5f;
        private float vignIntensity = 0;
        private Tween vignTween;
        #endregion
        #endregion
        #region Start() and Update()
        void Start() // Use this for initialization
        {
            //neighbourWoodsDatabase.variables.
            player = GameObject.FindWithTag("Player");
            playerCamera = GameObject.FindWithTag("MainCamera");
            playerCameraTransform = Camera.main.transform;
            smellOVision = GetComponentInChildren<PostProcessingBehaviour>();
            vision = Vision.NORMAL;
            gameState = GameState.FREE_ROAM;
            characterController = GetComponent<CharacterController>();
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        public void Update() // Update is called once per frame
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            bool running = Input.GetButton("Sprint"); // Sprinting in Game
            switch (gameState)
            {
                case GameState.FREE_ROAM: // if the GameState enum is in FreeRoam then all of the movement and button controls updates
                    MovementController(inputDir, running);
                    VisionController();
                    CameraController();
                    BarkController();
                    DigController();
                    JumpController();
                    InventoryController();
                    break;
                case GameState.DIALOGUE: // if the GameState enum is in Dialogue then the DialogueController() updates 
                    DialogueController();
                    break;
                case GameState.TITLE_SCREEN:
                    break;
                case GameState.PAUSE_SCREEN:
                    break;
                case GameState.CREDIT_SCREEN:
                    break;
                default: break;
            }
        }
        #endregion
        #region Control Methods
        #region MovementController()
        void MovementController(Vector2 inputDir, bool running) // The Function that moves the Player 
        {
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + playerCameraTransform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
            }
            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));
            velocityY += Time.deltaTime * gravity;
            Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
            characterController.Move(velocity * Time.deltaTime);
            if (characterController.isGrounded)
            {
                velocityY = 0;
            }
            #region Animator?
            // Animator?
            //float animationSpeedPercent = ((running) ? currentSpeed/runSpeed : currentSpeed/walkSpeed* .5f);
            //animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
            //currentSpeed = new Vector2(characterController.velocity.x, characterContoller.velocity.z).magnitude;
            #endregion
        }
        float GetModifiedSmoothTime(float smoothTime) // The Function that allows me to control the amount of movement that the player has when in the air
        {
            if (characterController.isGrounded)
            {
                return smoothTime;
            }
            if (airControlPercent == 0)
            {
                return float.MaxValue;
            }
            return smoothTime / airControlPercent;
        }
        #endregion
        #region VisionController() and Associated Methods()
        void VisionController() // The Function that switches the Vision enum event to smell or normal so that the colour blind camera comes on and the smell objects are turn on or off
        {
            if (Input.GetButtonDown("Smell-O-Vision"))
            {
                if (vision == Vision.NORMAL)
                {
                    vision = Vision.SMELL;
                }
                else
                {
                    vision = Vision.NORMAL;
                }
                GameEvents.ReportVisionChange(vision);
            }
        }
        public void AnimateVignette(bool value)
        {
            vignTween = DOTween.To(() => vignIntensity, x => vignIntensity = x, value ? maxVignIntensity : 0, vignDuration).OnUpdate(UpdateVignetteAnimation);
            vignTween.OnComplete(OnVignetteAnimationEnd);
        }
        void UpdateVignetteAnimation()
        {
            VignetteModel.Settings vign = smellOVision.profile.vignette.settings;
            vign.intensity = vignIntensity;
            smellOVision.profile.vignette.settings = vign;
        }
        void OnVignetteAnimationEnd()
        {
            vignTween = null;
        }
        #endregion
        #region CameraController()
        void CameraController() // Controls the third person camera
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            Vector3 targetRotation = new Vector3(pitch, yaw);
            float rotationSmoothTime = 0.12f;
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            playerCamera.transform.eulerAngles = targetRotation;
        }
        #endregion
        #region JumpController()
        void JumpController()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (characterController.isGrounded)
                {
                    float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                    velocityY = jumpVelocity;
                }
            }
        }
        #endregion
        #region DigController()
        void DigController()
        {
            if (Input.GetButtonDown("Dig"))
            {
                Debug.Log("Test Digging");
                //testTMP.text = ("Test Digging to be coded");
            }
        }
        #endregion
        #region BarkController()
        void BarkController()
        {
            if (Input.GetButtonDown("Bark"))
            {
                Debug.Log("Bark");
                //testTMP.text = ("Bark to be coded");
            }
        }
        #endregion
        #region InventoryController()
        void InventoryController()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                InventoryManager.instance.inventoryShowing = !InventoryManager.instance.inventoryShowing;
                GameEvents.ReportOnInventoryChange(InventoryManager.instance.inventoryShowing);
            }
        }
        #endregion
        #region MapController()
        void MapController()
        {
            if (Input.GetButtonDown("Map"))
            {
                Debug.Log("Map");
                //testTMP.text = ("Map to be coded");
            }
        }
        #endregion
        void DialogueController()
        {

        }
        #endregion
        #region Vision Event Methods
        void OnEnable() //Subscribes to our game events
        {
            GameEvents.OnVisionChange += OnVisionChange;
        }
        void OnDisable() //Unsubscribes to our game events
        {
            GameEvents.OnVisionChange -= OnVisionChange;
        }
        //float visionLevel;
        void OnVisionChange(Vision vision)
        {
            //Debug.Log("vision mode");
            if (vision == Vision.NORMAL)
            {
                Camera.main.GetComponent<GenericImageEffect>().enabled = false;
                //DOTween.To(() => smellOVision.profile.vignette.settings.color, x => smellOVision.profile.vignette.settings.color = x, 5, 1);
                AnimateVignette(false);
                foreach (GameObject smellObject in smellObjects)
                {
                    smellObject.SetActive(false);
                }
            }
            else
            {
                Camera.main.GetComponent<GenericImageEffect>().enabled = true;
                AnimateVignette(true);
                foreach (GameObject smellObject in smellObjects)
                {
                    smellObject.SetActive(true);
                }
            }
        }
        #endregion
    }
    #endregion
}