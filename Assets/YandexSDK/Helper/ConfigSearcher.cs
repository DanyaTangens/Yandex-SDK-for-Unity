using UnityEditor;
using UnityEngine;

namespace YandexSDK.Helper
{
    public class ConfigSearcher : MonoBehaviour
    {
#if UNITY_EDITOR
        private static string patchYGPrefab = "Assets/YandexSDK/Prefabs/YandexGame.prefab";

        public static Config GetConfig()
        {
            GameObject yaPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(patchYGPrefab, typeof(GameObject));
            if (yaPrefab == null)
            {
                Debug.LogError($"Префаб YandexGame не был найден по пути: {patchYGPrefab}");
                return null;
            }

            YandexGame yandexGame = yaPrefab.GetComponent<YandexGame>();
            if (yandexGame == null)
            {
                Debug.LogError($"На объекте YandexGame не был найден компонент YandexGame! Префаб объекта расположен по пути: {patchYGPrefab}");
                return null;
            }

            var config = yandexGame.Config;
            if (config == null)
            {
                Debug.LogError($"На компоненте YandexGame не определено поле config! Префаб YandexGame расположен по пути: {patchYGPrefab}");
                return null;
            }

            return config;
        }
#endif        
    }
}