using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace YandexSDK
{
    public partial class YandexGame : MonoBehaviour
    {
        public static YandexGame Instance;

        [SerializeField] private Config config;

        private bool isAuth;
        
        private bool isSdkEnabled;
        
        public Config Config => config;
        public bool IsAuth => isAuth;

        [Header("Events")]
        public UnityEvent onRejectedAuthorization;
        public UnityEvent onResolvedAuthorization;
        public static Action onGetDataEvent;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } else if (Instance != this)
            {
                Destroy(gameObject);    
            }
            
            DontDestroyOnLoad(gameObject);
            
            if (isSdkEnabled == false)
            {
                CallYaInitAttribute();
            }
            
        }
        
        private void Start()
        {
            if (isSdkEnabled == false)
            {
                CallYaStartAttribute();
                isSdkEnabled = true;
                GetDataInvoke();
#if !UNITY_EDITOR
                InitGameInternal();
#endif
            }
        }
        
        private void GetDataInvoke()
        {
            if (isSdkEnabled)
                onGetDataEvent?.Invoke();
        }
        
        private static void Message(string message)
        {
#if UNITY_EDITOR
            if (Instance.Config.isDebug)
#endif
                Debug.Log(message);
        }
        
        [DllImport("__Internal")]
        private static extern void InitGameInternal();
    }   
}
