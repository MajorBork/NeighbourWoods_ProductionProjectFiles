using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Manager.Level;
using Manager.Audio;
namespace Manager.Environment
{

    public class EnvironmentManager : Singleton<EnvironmentManager>
    {
        #region Variables
        [Header("Script Object References")]
        
        public Day day;
        public TimeSlot timeSlot;
        public Level.LevelManager levelManager;
        public AudioManager audioManager;
        public GameManager gameManager;
        public string lastMusicPlaying;
        [Header("Day Light Colour Setting")]
        public Light sceneLight;
        public Color morningLightColour;
        public Color middayLightColour;
        public Color afternoonLightColour;
        public Color eveningLightColour;
        [Header("TimeSlot Sky box Setting")]
        public Material morningSkyBox;
        public Material middaySkyBox;
        public Material afternoonSkyBox;
        public Material eveningSkyBox;
        #endregion
        void Start()// Use this for initialization
        {

        }
        void Update()// Update is called once per frame
        {
            
        }
        #region OnTimeChange Event Listerers To be used later?
        void OnEnable()
        {
            GameEvents.OnTimeChange += OnTimeChange;
            GameEvents.OnGameStateChange += OnGameStateChange;
        }
        void OnDisable()
        {
            GameEvents.OnTimeChange -= OnTimeChange;
            GameEvents.OnGameStateChange -= OnGameStateChange;
        }
        void OnGameStateChange(GameState gameState)
        {
            switch (gameManager.gameState)
            {
                case GameState.FREE_ROAM:
                    audioManager.PlayAudio(lastMusicPlaying);
                    break;
                case GameState.DIALOGUE:
                    audioManager.PlayAudio(lastMusicPlaying);
                    break;
                case GameState.TITLE_SCREEN:
                    audioManager.PlayAudio("sonk");
                    break;
                case GameState.PAUSE_SCREEN:
                    audioManager.PlayAudio("sonk");
                    break;
                case GameState.CREDIT_SCREEN:
                    audioManager.PlayAudio("sonk");
                    break;
                default:
                    break;
            }
        }
        void OnTimeChange(TimeSlot timeSlot, Day day)
        {
            StartCoroutine(ChangeTime());
        }
        IEnumerator ChangeTime ()
        {
            yield return new WaitForSeconds (2);
            switch (levelManager.timeSlot)
            {
                case TimeSlot.MORNING: // if the GameState enum is in FreeRoam then all of the movement and button controls updates
                    sceneLight.color = morningLightColour;
                    RenderSettings.skybox = morningSkyBox;
                    audioManager.PlayAudio("Town - Day");
                    lastMusicPlaying = "Town - Day";
                    break;
                case TimeSlot.MIDDAY:
                    sceneLight.color = middayLightColour;
                    RenderSettings.skybox = middaySkyBox;
                    audioManager.PlayAudio("Town - Day");
                    lastMusicPlaying = "Town - Day";
                    break;
                case TimeSlot.AFTERNOON:
                    sceneLight.color = afternoonLightColour;
                    RenderSettings.skybox = afternoonSkyBox;
                    audioManager.PlayAudio("Town - Night");
                    lastMusicPlaying = "Town - Night";
                    break;
                case TimeSlot.EVENING:
                    sceneLight.color = eveningLightColour;
                    RenderSettings.skybox = eveningSkyBox;
                    audioManager.PlayAudio("Town - Night");
                    lastMusicPlaying = "Town - Night";
                    break;
                default: break;
            }
        }
        #endregion
    }
}
