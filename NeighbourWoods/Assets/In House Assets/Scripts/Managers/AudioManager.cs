using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Manager.Player;
using EasyEditor;
namespace Manager.Audio
{
    #region AudioType Enum
    public enum AudioType
    {
        CHARACTER,
        CHARACTER_DIALOGUE,
        PLAYER_DIALOGUE,
        PLAYER,
        SOUND_EFFECT,
        GAME,
        MUSIC,
    }
    #endregion
    #region AudioManager Class
    public class AudioManager : Singleton<AudioManager>
    {
        //AudioFiles[] audioFiles;
        public float volume;
        public static bool mute;
        void OnEnable()  //Subscribes to our game events
        {
            GameEvents.OnVisionChange += OnVisionChange;
        }
        void OnDisable() //Unsubscribes to our game events
        {
            GameEvents.OnVisionChange -= OnVisionChange;
        }
        void OnVisionChange(Vision vision)
        {
            Debug.Log("DOG SOUND");
        }
    }
    #endregion
    #region AudioFiles Class
    [Groups("Base Settings")]
    [System.Serializable]
    public class AudioFiles
    {
        public AudioClip audioClip;
        public AudioType audioType;
    }
    #endregion
}
