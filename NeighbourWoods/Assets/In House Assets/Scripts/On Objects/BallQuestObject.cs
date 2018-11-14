using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using Manager.Inventory;
using Manager.UI;

public class BallQuestObject : MonoBehaviour
{
    //private string inventoryManagerMethodPickupItem = "PickupItem";
    public string questTitle;
    //private string inventoryManagerUpdateItemIcon = "OnItemPickup";
    private QuestState newQuestState = QuestState.Active;
    //private QuestState newQuestState = QuestState.Success;
    public GameObject questObject;
    public string message;
    //public Image ballIcon;
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.SendMessage("UpdateItemIcon");
            QuestLog.SetQuestState(questTitle, newQuestState);
            DialogueManager.ShowAlert(message);
            //inventoryManager.SendMessage(inventoryManagerMethodPickupItem, bananaIcon);
            questObject.SetActive(false);
        }
    }
}
