using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyEditor;
using Manager.Level;
using PixelCrushers.DialogueSystem;
using Language.Lua;
namespace Manager.Environment
{

    public class EnvironmentManager : Singleton<EnvironmentManager>
    {
        #region Variables
        public Day day;
        public TimeSlot timeSlot;
        public Level.LevelManager levelManager;
        public Light sceneLight;
        public Color morningLightColour;
        public Color middayLightColour;
        public Color afternoonLightColour;
        public Color eveningLightColour;
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
        }
        void OnDisable()
        {
            GameEvents.OnTimeChange -= OnTimeChange;
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
                    break;
                case TimeSlot.MIDDAY:
                    sceneLight.color = middayLightColour;
                    RenderSettings.skybox = middaySkyBox;
                    break;
                case TimeSlot.AFTERNOON:
                    sceneLight.color = afternoonLightColour;
                    RenderSettings.skybox = afternoonSkyBox;
                    break;
                case TimeSlot.EVENING:
                    sceneLight.color = eveningLightColour;
                    RenderSettings.skybox = eveningSkyBox;
                    break;
                default: break;
            }
        }
        #endregion
    }
    #region Environment Class
    //[Groups("Base Settings")]
    //[System.Serializable]
    //public class Environments
    //{

    //}
    #endregion
}
