﻿using System.Collections;
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
        //public CanvasGroup dialogueBoxCanvas;
        //public CanvasGroup buttonPressBox;
        public CanvasGroup fadeCanvas;
        public CanvasGroup inventoryCanvas;
        //TextMeshProUGUI object variables
        public TextMeshProUGUI dayText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI tmpFoodText;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI dogBoneText;
        //Image Variables
        public Icons icons;
        public Image itemImageUI;
        // other Variables
        public GameState gameState;
        public int taskPoints;
        public int newTaskPoints;
        public int dogBoneCollected;
        public string textFood;
        public string itemNameString;
        public int textFoodInt;
        public float fadeInTime = 1;
        public Image characterFriendshipIcon;
        
        #endregion
        #region Start and Update
        void Start() // Use this for initialization
        {
            
            //InventoryCanvas.alpha = 0;
            //buttonPressBox.alpha = 0;
            fadeCanvas.alpha = 0;
            timeText.text = "";
            dayText.text = "";
            taskPoints = 0;
            
            //Debug.Log(DSfood);
        }
        void Update() // Update is called once per frame
        {
            //OnTaskPointsChange(taskPoints);
            //Debug.Log(taskPoints);
            
        }
        #endregion
        #region Methods
        public void UpdateItem(string incomingItemName) // updates the item Icon in the inventory
        {
            switch (incomingItemName)
            {
                case "Newspaper":
                    itemImageUI.sprite = icons.newspaperIcon;
                    break;
                case "Clue":
                    itemImageUI.sprite = icons.clueIcon;
                    break;
                default: itemImageUI.sprite = null;
                    break;
            }
            OnPlayerObjects.instance.EnableItem(incomingItemName);
            // replaces inventoryVis icon 
            itemNameString = incomingItemName;
            itemName.text = itemNameString;
            //Debug.Log("hey I am working");
        }
        public void GetRidOfItem() // trying to get rid of the icon
        {
            Debug.Log("Getting rid of items");
            //itemImageUI.enabled = false;
            itemNameString = "";
            itemName.text = itemNameString;
            itemImageUI.sprite = null;
            OnPlayerObjects.instance.DisableAll();
            
        }
        public void OnCharacterTalk (int friendshipPoint)
        {
            if (friendshipPoint < -4)
            {
                characterFriendshipIcon.color = Color.red;
            }
            if (friendshipPoint >= -4 && friendshipPoint <= 4)
            {
                characterFriendshipIcon.color = Color.grey;
            }
            if(friendshipPoint > 4)
            {
                characterFriendshipIcon.color = Color.green;
            }
        }
        public void UpdateCharacterFriendship(string characterTalking)
        {

        }
        public void UpdateFoodText(int food) // trying to update food text
        {
            tmpFoodText.text = ("Food: " + food);
        }
        public void DogBoneUpdate()
        {
            dogBoneCollected++;
            dogBoneText.text = "Bones: " + dogBoneCollected + "/20";
        }
        #endregion
        #region Listeners 
        // Listeners for Game Events
        void OnEnable() 
        {
            GameEvents.OnGameStateChange += OnGameStateChange;
            GameEvents.OnTimeChange += OnTimeChange;
            GameEvents.OnCharacterTalk += OnCharacterTalk;
        }
        void OnDisable()
        {
            GameEvents.OnGameStateChange -= OnGameStateChange;
            GameEvents.OnTimeChange -= OnTimeChange;
            GameEvents.OnCharacterTalk -= OnCharacterTalk;
        }
        #endregion
        #region OnGameStateChange
        void OnGameStateChange(GameState gameState) // On game state change with the enum GameState
        {
            if (gameState == GameState.DIALOGUE)
            {
                //dialogueBoxCanvas.alpha = 1;
            }
            else
            {
                //dialogueBoxCanvas.alpha = 0;
            }
        }
        #endregion
        #region OnTimeChange
        void OnTimeChange(TimeSlot timeSlot, Day day) // On time change with the enum TimeSlot and Day
        {
            StartCoroutine(WhileFadeCanvas(timeSlot, day));
        }
        IEnumerator WhileFadeCanvas(TimeSlot timeSlot, Day day)
        {
            fadeCanvas.DOFade(1, fadeInTime);
            yield return new WaitForSeconds(fadeInTime * 2);
            //string temp = timeSlot.ToString();
            //temp.ToLower();
            timeText.text = FormatDay(timeSlot.ToString());
            dayText.text = FormatDay(day.ToString());
            yield return new WaitForSeconds(fadeInTime * 2);
            fadeCanvas.DOFade(0, fadeInTime);
            yield return new WaitForSeconds(1);
            timeText.text = "";
            dayText.text = "";
        }
        string FormatDay(string incoming)
        {
            switch (incoming)
            {
                case "MORNING":
                    return "Morning";
                case "AFTERNOON":
                    return "Afternoon";
                case "MIDDAY":
                    return "Midday";
                case "EVENING":
                    return "Evening";
                case "DAY_1":
                    return "Day 1";
                case "DAY_2":
                    return "Day 2";
                case "DAY_3":
                    return "Day 3";
                case "DAY_4":
                    return "Day 4";
                case "DAY_5":
                    return "Day 5";
                case "DAY_6":
                    return "Day 6";
                case "DAY_7":
                    return "Day 7";
                default: return "";
            }
        }
        #endregion
    }
    #endregion
}
