using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using Manager.Inventory;
using Manager.UI;

public class BallQuestObject : MonoBehaviour
{
    //private string inventoryManagerMethodPickupItem = "PickupItem";
    private string questTitle = "BallQuest";
    private string inventoryManagerMethodUpdateItemIcon = "UpdateItemIcon";
    private QuestState newQuestState = QuestState.Success;
    private QuestState currentQuestState = QuestState.Active;
    public GameObject ballObject;
    public string message;
    public Image ballIcon;
    public InventoryManager inventoryManager;
    public UIManager uiManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.SendMessage(inventoryManagerMethodUpdateItemIcon);
            QuestLog.SetQuestState(questTitle, newQuestState);
            DialogueManager.ShowAlert(message);
            //inventoryManager.SendMessage(inventoryManagerMethodPickupItem, bananaIcon);
            ballObject.SetActive(false);
        }
    }
}
