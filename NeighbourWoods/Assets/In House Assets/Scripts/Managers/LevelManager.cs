using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
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
    #region LevelManager Class
    public class LevelManager : Singleton<LevelManager>
    {
        #region Variables
        //public Levels[] levels;
        public Day day;
        public TimeSlot timeSlot;
        public GameObject[] day1morningObjects;
        public GameObject[] day1middayObjects;
        public GameObject[] day1afternoonObjects;
        public GameObject[] day1eveningObjects;
        public GameObject[] day2morningObjects;
        public GameObject[] day2middayObjects;
        public GameObject[] day2afternoonObjects;
        public GameObject[] day2eveningObjects;
        public int currentTime;
        public int maxTaskPoints = 4;
        #endregion
        #region Start and Update Methods
        void Start() // Use this for initialization
        {
            timeSlot = TimeSlot.MORNING;
            day = Day.DAY_1;
            OnTimeChange(timeSlot, day);
        }
        void Update() // Update is called once per frame
        {

        }
        #endregion
        public void CheckTime() // function to check if task points is reached
        {
            Debug.Log("I am Working");
            int DStaskPoints = DialogueLua.GetVariable("TaskPoints").asInt;
            if (DStaskPoints >= maxTaskPoints)
            {
                StartCoroutine(UpdateTime());
                //UpdateTime();
            }
        }
        public void DebugUpdateTime()
        {
            timeSlot++;
            GameEvents.ReportGameStateChange(GameState.FREE_ROAM);
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
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        #region Time Methods
        public IEnumerator UpdateTime() // updates our time to the next increment 
        {
            yield return new WaitForSeconds(3);
            timeSlot++;
            GameEvents.ReportGameStateChange(GameState.FREE_ROAM);
            if ((int)timeSlot == 4)
            {
                day++;    
                timeSlot = TimeSlot.MORNING;
                if ((int)day == 3)
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
        #region Utility Time Methods
        public void UpdateTimeToMorningDay1()
        {
            if (day == Day.DAY_2)
            {
                day = Day.DAY_1;
            }
            if (timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.AFTERNOON || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.MORNING;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToMiddayDay1()
        {
            if (day == Day.DAY_2)
            {
                day = Day.DAY_1;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.AFTERNOON || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.MIDDAY;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToAfternoonDay1()
        {
            if (day == Day.DAY_2)
            {
                day = Day.DAY_1;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.AFTERNOON;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToEveningDay1()
        {
            if (day == Day.DAY_2)
            {
                day = Day.DAY_1;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.AFTERNOON)
            {
                timeSlot = TimeSlot.EVENING;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToMorningDay2()
        {
            if (day == Day.DAY_1)
            {
                day = Day.DAY_2;
            }
            if (timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.AFTERNOON || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.MORNING;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToMiddayDay2()
        {
            if (day == Day.DAY_1)
            {
                day = Day.DAY_2;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.AFTERNOON || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.MIDDAY;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToAfternoonDay2()
        {
            if (day == Day.DAY_1)
            {
                day = Day.DAY_2;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.EVENING)
            {
                timeSlot = TimeSlot.AFTERNOON;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        public void UpdateTimeToEveningDay2()
        {
            if (day == Day.DAY_1)
            {
                day = Day.DAY_2;
            }
            if (timeSlot == TimeSlot.MORNING || timeSlot == TimeSlot.MIDDAY || timeSlot == TimeSlot.AFTERNOON)
            {
                timeSlot = TimeSlot.EVENING;
            }
            else
            {
                return;
            }
            GameEvents.ReportOnTimeChange(timeSlot, day);
        }
        #endregion
        #endregion
        #region OnTimeChange
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
            if (timeSlot == TimeSlot.MORNING && day == Day.DAY_1)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(true); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false);
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day1afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day1eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.MIDDAY && day == Day.DAY_1)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(true);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day2afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day2eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.AFTERNOON && day == Day.DAY_1)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(true);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day2afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day2eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.EVENING && day == Day.DAY_1)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(true);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day2afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day2eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.MORNING && day == Day.DAY_2)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(true); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day1afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day1eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.MIDDAY && day == Day.DAY_2)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(true);
                }
                foreach (GameObject day2afternoonObject in day2afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day2eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.AFTERNOON && day == Day.DAY_2)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day2afternoonObjects)
                {
                    day2afternoonObject.SetActive(true);
                }
                foreach (GameObject day2eveningObject in day2eveningObjects)
                {
                    day2eveningObject.SetActive(false);
                }
            }
            if (timeSlot == TimeSlot.EVENING && day == Day.DAY_2)
            {
                foreach (GameObject day1morningObject in day1morningObjects)
                {
                    day1morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day1middayObject in day1middayObjects)
                {
                    day1middayObject.SetActive(false);
                }
                foreach (GameObject day1afternoonObject in day1afternoonObjects)
                {
                    day1afternoonObject.SetActive(false);
                }
                foreach (GameObject day1eveningObject in day1eveningObjects)
                {
                    day1eveningObject.SetActive(false);
                }
                foreach (GameObject day2morningObject in day2morningObjects)
                {
                    day2morningObject.SetActive(false); // to change into turning off visual components because characters need to be updated when not visualable
                }
                foreach (GameObject day2middayObject in day2middayObjects)
                {
                    day2middayObject.SetActive(false);
                }
                foreach (GameObject day2afternoonObject in day2afternoonObjects)
                {
                    day2afternoonObject.SetActive(false);
                }
                foreach (GameObject day2eveningObject in day2eveningObjects)
                {
                    day2eveningObject.SetActive(true);
                }
            }
        }
        #endregion
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
