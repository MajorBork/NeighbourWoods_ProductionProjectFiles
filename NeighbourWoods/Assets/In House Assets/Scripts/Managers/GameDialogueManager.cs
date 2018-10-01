using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using EasyEditor;
using PixelCrushers.DialogueSystem;
using Language.Lua;
using Manager.Character;
using Manager.Environment;
using Manager;
namespace Manager.GameDialogue
{
    public class GameDialogueManager : MonoBehaviour
    {
        #region Variables
        public EnvironmentManager environmentManager;
        public GameManager gameManager;
        public int food;
        public int taskPoints;
        public GameState gameState;
        #endregion
        #region Start and Update Methods
        void Start() // Use this for initialization
        {

        }
        void Update() // Use this for initialization
        {

        }
        #endregion
        #region Start and EndDialogue Methods
        public void StartDialogue()
        {
            Debug.Log("Start of converstion.");
            if (gameState == GameState.FREE_ROAM)
            {
                gameState = GameState.DIALOGUE;
                Debug.Log(gameState);
            }
            GameEvents.ReportGameStateChange(gameState);
        }
        public void EndDialogue()
        {
            Debug.Log("End of converstion.");
            if (gameState == GameState.DIALOGUE)
            {
                gameState = GameState.FREE_ROAM;
                Debug.Log(gameState);
            }
            GameEvents.ReportGameStateChange(gameState);
        }
        #endregion
    }
    #region DialogueList Class
    //[Groups("Base Settings")]
    //[System.Serializable]
    //public class DialogueList
    //{

    //}
    #endregion
}
