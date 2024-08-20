using System.Runtime.InteropServices;
using UnityEngine;

namespace YandexSDK
{
    public partial class YandexGame
    {
        private bool isGamePlaying;
        private bool isGameReadyUsed;

        public bool IsGamePlaying => isGamePlaying;
        
        [YaStart]
        public void InitGameReady()
        {
            if (Instance.Config.isNeedAutoGameReadyStart)
            {
                GameReady();   
            }
        }
        
        private void GameReady()
        {
            if (isGameReadyUsed) return;
            
            isGameReadyUsed = true;
            Message("Gameplay ready");
#if !UNITY_EDITOR
                GameplayReadyInternal();
#endif
        }
        
        public void GameplayStart()
        {
            if (isGamePlaying)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Gameplay already started");
#endif
                return;
            }
            
            isGamePlaying = true;
            Message("Gameplay started");
#if !UNITY_EDITOR
                GameplayStartInternal();
#endif
        }
        
        public void GameplayStop()
        {
            if (!isGamePlaying)
            {
#if UNITY_EDITOR
               Debug.LogWarning("Gameplay already stopped");
#endif
                return;
            }
            
            isGamePlaying = false;
            Message("Gameplay stopped");
#if !UNITY_EDITOR
                GameplayStopInternal();
#endif
        }
 
        [DllImport("__Internal")]
        private static extern void GameplayReadyInternal();
        [DllImport("__Internal")]
        private static extern void GameplayStartInternal();
        [DllImport("__Internal")]
        private static extern void GameplayStopInternal();
    }
}