using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Item;
using Manager.UI;
using DG.Tweening;
using TMPro;

namespace Manager.Inventory 
{
    #region InventoryVis Enum
    public enum InventoryVis
    {
        IS_LOOKING_IN_INVENTORY,
        NOT_LOOKING_IN_INVENTORY,
    }
    #endregion
    #region InventoryManager Class
    public class InventoryManager : Singleton<InventoryManager>
    {
        //public Items iteminInventory;
        public UIManager gameManagerUI;
        public GameObject itemObject;
        public bool inventoryShowing;
        public bool hasItem;
        public string uiManagerMethod = "UpdateFoodText";
        public string masterFood;
        void Start() // Use this for initialization
        {
            inventoryShowing = false;
            gameManagerUI.inventoryCanvas.alpha = 0;
        }
        void Update() // Update is called once per frame
        {
            
        }
        #region Food Methods
        public void AddFood(string food)
        {
            food = masterFood;
            SendMessage(uiManagerMethod,masterFood);
        }
        public void MinusFood(string food)
        {
            food = masterFood;
            SendMessage(uiManagerMethod, masterFood);
        }
        #endregion
        #region Inventory Event Methods
        void OnEnable()
        {
            GameEvents.OnInventoryVisChange += OnShowInventoryChange;
        }
        void OnDisable()
        {
            GameEvents.OnInventoryVisChange -= OnShowInventoryChange;
        }
        void OnShowInventoryChange(bool inventoryVis)
        {
            if (inventoryVis)
            {
                gameManagerUI.inventoryCanvas.DOFade(1, 0.3f);
                gameManagerUI.inventoryCanvas.interactable = true;
                gameManagerUI.inventoryCanvas.blocksRaycasts = true;
            }
            else
            {
                gameManagerUI.inventoryCanvas.DOFade(0, 0.3f);
                gameManagerUI.inventoryCanvas.interactable = false;
                gameManagerUI.inventoryCanvas.blocksRaycasts = false;
            }
        }
        #endregion
    }
    #endregion
}
