using UnityEngine;

namespace YandexSDK
{
    public class YandexGame : MonoBehaviour
    {
        public static YandexGame Instance;
        
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
