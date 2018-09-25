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
    public class PlayerManager : Singleton<PlayerManager> // An script that will not be destroyed when going new levels (if we need to load new levels)
    {
        #region Variables
        // Enums
        [Tooltip("The public Vision enum variable on the player object")]
        public Vision vision;
        [Tooltip("The public GameState enum variable on the player object")]
        public GameState gameState;
        #region Object Variables
        // All of the Variables that link to objects or components
        [Tooltip("The GameObject variable that is the player object in the scene")]
        public GameObject player;
        [Tooltip("The variable that references the UIManager script on the canvas")]
        public UIManager gameManagerUI;
        [Tooltip("The variable that references the GameManager Script on the GameManager object")]
        public GameManager gameManager;
        [Tooltip("The variable that references the PlayerCamera script on the ColourBlind camera object")]
        public GameObject playerCamera;
        [Tooltip("The variable that references the transform of the playerCamera")]
        public Transform playerCameraTransform;
        [Tooltip("The PostProcessing Profile of smellOVision variable")]
        public PostProcessingBehaviour smellOVision;
        [Tooltip("The variable that references the Character Controller component on the player object")]
        public CharacterController characterController;
        [Tooltip("The array variable that holds smellObject")]
        public GameObject[] smellObjects = new GameObject[0];
        #endregion
        #region Control Variables
        //Space//
        //All of the controls variables used in the movement functions
        [Tooltip("The sensitivity of the Mouse for the camera movements")]
        public float mouseSensitivity = 10;
        [Tooltip("The speed of the walk of the character")]
        public float walkSpeed = 2;
        [Tooltip("The spped of the run of the character")]
        public float runSpeed = 6;
        [Tooltip("The gravity of the character when in air")]
        public float gravity = -12;
        [Tooltip("The jump height of the character")]
        public float jumpHeight = 1;
        [Tooltip("The percent of the player control in the air")]
        [Range(0, 1)]
        public float airControlPercent;
        private float velocityY;
        [Tooltip("")]
        public float turnSmoothTime = 0.2f;
        [Tooltip("")]
        public float speedSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        private float speedSmoothVelocity;
        private float currentSpeed;
        [Tooltip("")]
        public bool lockCursor;
        private Vector3 currentRotation;
        private Vector3 rotationSmoothVelocity;
        [Tooltip("")]
        public Vector2 pitchMinMax = new Vector2(-40,85);
        [Tooltip("")]
        public float cameraRotation;
        [Tooltip("")]
        public Transform target;
        private float yaw = 0;
        private float pitch = 0;
        #endregion
        #region Vision Variables
        [Tooltip("")]
        [SerializeField]
        public float maxVignIntensity = 0.3f;
        [Tooltip("")]
        [SerializeField]
        public float vignDuration = 0.5f;
        [Tooltip("")]
        private float vignIntensity = 0;
        [Tooltip("")]
        private Tween vignTween;
        #endregion
        #endregion
        #region Start() and Update()
        void Start() // Use this for initialization
        {
            // Setting 
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
        public void AnimateVignette(bool value) // Calucating the animation for the vignette for Smell-O-Vision
        {
            vignTween = DOTween.To(() => vignIntensity, x => vignIntensity = x, value ? maxVignIntensity : 0, vignDuration).OnUpdate(UpdateVignetteAnimation);
            vignTween.OnComplete(OnVignetteAnimationEnd);
        }
        void UpdateVignetteAnimation() // Updating the animation for the vignette
        {
            VignetteModel.Settings vign = smellOVision.profile.vignette.settings;
            vign.intensity = vignIntensity;
            smellOVision.profile.vignette.settings = vign;
        }
        void OnVignetteAnimationEnd() // At the end of the animation
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
        void JumpController() // Controls the jump functions 
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
        void InventoryController() // Contols the inventory functions 
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
        void OnVisionChange(Vision vision) // OnVisionChange function that run if statements if the vision state is in normal or smell
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