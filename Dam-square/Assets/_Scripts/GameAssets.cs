using System;
using UnityEngine;

namespace _Scripts
{
    public class GameAssets : MonoBehaviour
    {
        #region Singleton
        private static GameAssets instance;
        public static GameAssets Instance { get { return instance; } }
        #endregion
    
        #region Fields
        public SoundAudioClip[] soundAudioClipArray;
    
        [Serializable]
        public class SoundAudioClip
        {
            public SoundManager.Sound sound;
            public AudioClip audioClip;
        }

        #endregion
    

        #region Unity Methods
        private void Awake()
        {
            #region Singleton
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            } else {
                instance = this;
            }
            #endregion
        }
        #endregion
    }
}
