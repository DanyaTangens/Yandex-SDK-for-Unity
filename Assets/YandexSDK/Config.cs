using UnityEngine;
using YandexSDK.Enum;

namespace YandexSDK
{
    [CreateAssetMenu(fileName = "YaConfig", menuName = "Yandex Game/Create config")]
    public class Config: ScriptableObject
    {
        [Header("Базовые настройки")]
        
        [Tooltip("Размер изображения пользователя")]
        public PlayerPhotoSizeEnum playerPhotoSize;
        
        [Header("Доска лидеров?")]
        
        [Tooltip("Включить модуль доски лидеров?")]
        public bool isLeaderboardEnable = true;
        
        [Tooltip("Записывать рекорд анонимного игрока?")]
        public bool isNeedSaveScoreAnonymousPlayer = true;
        
        [Header("Другие настройки")]
        [Tooltip("Включить автоматическую архивацию билда?")]
        public bool isNeedArchivingBuild = true;

        public string GetPlayerPhotoSize()
        {
            return playerPhotoSize switch
            {
                PlayerPhotoSizeEnum.Small => "small",
                PlayerPhotoSizeEnum.Medium => "medium",
                PlayerPhotoSizeEnum.Large => "large",
                _ => null
            };
        }
    }
}