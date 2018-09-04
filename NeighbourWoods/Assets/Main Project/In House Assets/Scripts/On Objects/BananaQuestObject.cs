using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using Manager.Inventory;
using Manager.UI;

public class BananaQuestObject : MonoBehaviour
{
    //private string inventoryManagerMethodPickupItem = "PickupItem";
    private string questTitle = "BananaQuest";
    private string inventoryYManagerMethodUpdateItemIcon = "UpdateItemIcon";
    private QuestState newQuestState = QuestState.Success;
    public GameObject bananaObject;
    public string message;
    public Image bananaIcon;
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (Input.GetButtonDown("Dig"))
            //{
                //uiManager.SendMessage();
                QuestLog.SetQuestState(questTitle, newQuestState);
                DialogueManager.ShowAlert(message);
                //inventoryManager.SendMessage(inventoryManagerMethodPickupItem, bananaIcon);
                uiManager.SendMessage(inventoryYManagerMethodUpdateItemIcon, bananaIcon);
                bananaObject.SetActive(false);
                //uiManager.
            //}
        }
    }
}
