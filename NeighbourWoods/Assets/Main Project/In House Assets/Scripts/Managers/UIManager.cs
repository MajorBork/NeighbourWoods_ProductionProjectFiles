using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Manager.Level;
using Manager.Inventory;
using Manager;
using DG.Tweening;
using Language.Lua;
using PixelCrushers.DialogueSystem;
using TMPro;

namespace Manager.UI 
{
    #region UIManager Class
    public class UIManager : Singleton<UIManager>
    {
        #region Variables
        public CanvasGroup dialogueBoxCanvas;
        //public CanvasGroup buttonPressBox;
        public CanvasGroup fadeCanvas;
        public CanvasGroup inventoryCanvas;
        public TextMeshProUGUI dayText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI tmpFoodText;
        //public TextMeshProUGUI 
        public Image itemImageUI;
        public Image nothingItemImageUI;
        public GameState gameState;
        public int taskPoints;
        public int newTaskPoints;
        public int textFood;
        public float fadeInTime = 1;
        #endregion
        #region Start and Update
        void Start() // Use this for initialization
        {
            dialogueBoxCanvas.alpha = 0;
            //buttonPressBox.alpha = 0;
            fadeCanvas.alpha = 0;
            timeText.text = "";
            dayText.text = "";
            taskPoints = 0;
        }
        void Update() // Update is called once per frame
        {
            //OnTaskPointsChange(taskPoints);
            //Debug.Log(taskPoints);
        }
        #endregion
        #region Methods
        public void UpdateItemIcon(Image itemIcon)
        {
            // replaces inventoryVis icon 
            itemIcon = itemImageUI;
            nothingItemImageUI.enabled = false;
            itemImageUI.enabled = true;
        }
        public void GetRidOfItemIcon()
        {
            itemImageUI.enabled = false;
            nothingItemImageUI.enabled = true;
        }
        void UpdateFoodText(int food)
        {
            food = textFood;
            tmpFoodText.text = ("Food: " + textFood);
        }
        void UpdateCharacterIcon()
        {

        }
        #endregion
        #region Listeners
        void OnEnable()
        {
            GameEvents.OnGameStateChange += OnGameStateChange;
            GameEvents.OnTimeChange += OnTimeChange;
        }
        void OnDisable()
        {
            GameEvents.OnGameStateChange -= OnGameStateChange;
            GameEvents.OnTimeChange -= OnTimeChange;
        }
        #endregion
        #region OnGameStateChange
        void OnGameStateChange(GameState gameState)
        {
            if (gameState == GameState.DIALOGUE)
            {
                dialogueBoxCanvas.alpha = 1;
            }
            else
            {
                dialogueBoxCanvas.alpha = 0;
            }
        }
        #endregion
        #region OnTimeChange
        void OnTimeChange(TimeSlot timeSlot, Day day)
        {
            StartCoroutine(WhileFadeCanvas(timeSlot, day));
        }
        IEnumerator WhileFadeCanvas(TimeSlot timeSlot, Day day)
        {
            fadeCanvas.DOFade(1, fadeInTime);
            yield return new WaitForSeconds(fadeInTime * 2);
            //string temp = timeSlot.ToString();
            //temp.ToLower();
            timeText.text = timeSlot.ToString();
            dayText.text = day.ToString();
            yield return new WaitForSeconds(fadeInTime * 2);
            fadeCanvas.DOFade(0, fadeInTime);
            yield return new WaitForSeconds(1);
            timeText.text = "";
            dayText.text = "";
        }
        //void OnTaskPointsChange(LuaWatchItem luaWatchItem, Lua.Result newValue)
        //{
        //    DialogueManager.AddLuaObserver("Variable['TaskPoints']", LuaWatchFrequency.EveryDialogueEntry, OnTaskPointsChange);
        //    Debug.Log("Number of TaskPoints change to: " + newValue.AsInt);
        //}
        #endregion
    }
    #endregion
}
