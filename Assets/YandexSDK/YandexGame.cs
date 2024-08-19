using UnityEngine;

namespace YandexSDK
{
    public class YandexGame : MonoBehaviour
    {
        public static YandexGame Instance;

        [SerializeField] private Config config;

        public Config Config => config;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            } else if (Instance != this)
            {
                Destroy(gameObject);    
            }
            
            DontDestroyOnLoad(gameObject);
        }
    }   
}
