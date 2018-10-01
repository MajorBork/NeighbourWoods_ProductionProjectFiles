using System.Collections;
using System.Collections.Generic;
using EasyEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager.Task;
//using Manager.Player;
//using PixelCrushers.DialogueSystem;

namespace Manager.Level
{
    #region Days Enum
    public enum Day
    {
        DAY_1,
        DAY_2,
        DAY_3,
        DAY_4,
        DAY_5,
        DAY_6,
        DAY_7,
    }
    #endregion
    #region TimeSlot Enum
    public enum TimeSlot
    {
        MORNING,
        MIDDAY,
        AFTERNOON,
        EVENING,
    }
    #endregion
    #region LocationID Enum
    public enum LocationID
    {
        HOME,
        STREETS,
        HOUSES_OF_THE_NEIGHBOURHOOD,
        SHOBES_HOUSE,
        EBONYS_AND_ROGERS_HOUSE,
        OSCAR_MILY_AND_LIEAS_HOUSE,
        THE_CHICKENS_HOUSE,
        MURPHY_CALLIE_FRANGELINA_AND_GINGIES_HOUSE,
        ARCHIES_HOUSE,
        THE_TRIOS_HOUSE,
        BUNDY_AND_SNOWBALLS_HOUSE,
        MIDNIGHT_AND_GINGERS_HOUSE,
        MUFFLES_AND_KEGS_HOUSE,
        LILLAS_HOUSE,
        ABANDONED_HOUSE,
        WTICHS_HOUSE,
        HOUSE_1,
        HOUSE_2,
        HOUSE_3,
        HOUSE_4,
        HOUSE_5,
        HOUSE_6,
        HOUSE_7,
        HOUSE_8,
        HOUSE_9,
        HOUSE_10,
        CONCESSION_STORE,
        PARK,
        JUNKYARD,
        STRAYS_DEN,
        WOOD_TRAIL,
        THE_BRAMBLES,
        FOX_DEN,
        PINIC_AREA,
        THE_WARREN,
        THE_CLEARING,
        KELPIE_POOL,
        UNICORN_GLADE,
        THE_MYSTERIOUS_ENTITYS_LAIR,
    }
    #endregion
    #region LevelManager Class
    public class LevelManager : Singleton<LevelManager>
    {
        #region Variables
        //public Levels[] levels;
        public Day day;
        public TimeSlot timeSlot;
        public GameObject[] morningObjects;
        public GameObject[] middayObjects;
        public GameObject[] afternoonObjects;
        public GameObject[] eveningObjects;

        public int currentTime;
        public int maxtask;
        #endregion
        #region Start and Update Methods
        void Start() // Use this for initialization
        {
            timeSlot = TimeSlot.MORNING;
            day = Day.DAY_1;
            OnTimeChange(timeSlot,day);
        }
        void Update() // Update is called once per frame
        {

        }
        #endregion
        #region Time Methods
        public void UpdateTime() // updates our time to the next increment 
        {
            timeSlot++;
            if ((int)timeSlot == 4)
            {
                day++;    
                timeSlot = TimeSlot.MORNING;
                if ((int)day == 2)
                {
                    SceneManager.LoadScene("OverworldScene");
                }
                if ((int)day == 7)
                {
                    //make gameevent reportgameover 
                }
            }
            GameEvents.ReportOnTimeChange(timeSlot,day);
        }
        public void MorningTime()// to change into turning off visual components because characters need to be updated when not visualable but for prototype it will do
        {

        }
        #region Utility Time Methods
        public void UpdateTimeToMorning()
        {
            if (timeSlot == TimeSlot.MORNING)
            {
                return;
            }
            if (timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.AFTERNOON || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.MORNING;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToMidday()
        {
            if (timeSlot == TimeSlot.MIDDAY)
            {
                return;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.AFTERNOON || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.MIDDAY;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToAfternoon()
        {
            if (timeSlot == TimeSlot.AFTERNOON)
            {
                return;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.AFTERNOON;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToEvening()
        {
            if (timeSlot == TimeSlot.EVENING)
            {
                return;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.AFTERNOON)
            {
                timeSlot = TimeSlot.EVENING;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        #endregion
        #endregion
        void OnEnable() //Subscribes to our game events
        {
            GameEvents.OnTimeChange += OnTimeChange;
        }
        void OnDisable() //Unsubscribes to our game events
        {
            GameEvents.OnTimeChange -= OnTimeChange;
        }
        public void OnTimeChange(TimeSlot timeSlot, Day day)
        {
            if (timeSlot == TimeSlot.MORNING)
            {
                foreach (GameObject morningObject in morningObjects)
                {
                    morningObject.SetActive(true); // to change into turning off visual components because characters need to be updated when not visualable but for prototype it will do
                }
                foreach (GameObject middayObject in middayObjects)
                {
                    middayObject.SetActive(false);
                }
                foreach (GameObject afternoonObject in afternoonObjects)
                {
                    afternoonObject.SetActive(false);
                }
                foreach (GameObject eveningObject in eveningObjects)
                {
                    eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.MIDDAY)
            {
                foreach (GameObject morningObject in morningObjects)
                {
                    morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable but for prototype it will do
                }
                foreach (GameObject middayObject in middayObjects)
                {
                    middayObject.SetActive(true);
                }
                foreach (GameObject afternoonObject in afternoonObjects)
                {
                    afternoonObject.SetActive(false);
                }
                foreach (GameObject eveningObject in eveningObjects)
                {
                    eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.AFTERNOON)
            {
                foreach (GameObject morningObject in morningObjects)
                {
                    morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable but for prototype it will do
                }
                foreach (GameObject middayObject in middayObjects)
                {
                    middayObject.SetActive(false);
                }
                foreach (GameObject afternoonObject in afternoonObjects)
                {
                    afternoonObject.SetActive(true);
                }
                foreach (GameObject eveningObject in eveningObjects)
                {
                    eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.EVENING)
            {
                foreach (GameObject morningObject in morningObjects)
                {
                    morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable but for prototype it will do
                }
                foreach (GameObject middayObject in middayObjects)
                {
                    middayObject.SetActive(false);
                }
                foreach (GameObject afternoonObject in afternoonObjects)
                {
                    afternoonObject.SetActive(false);
                }
                foreach (GameObject eveningObject in eveningObjects)
                {
                    eveningObject.SetActive(true);
                }
            }
        }
    }
    #endregion
    #region Levels Class
    //[Groups("Base Settings")]
    //[System.Serializable]
    //public class Levels
    //{
    //    public LocationID locationID; 
    //    public BoxCollider areaCollider;
    //    public Tasks tasksInArea;
    //}
    #endregion
}
