using UnityEngine;
using YandexSDK.Entity;
using YandexSDK.Enum;
using YandexSDK.Example.Entity;

namespace YandexSDK
{
    [CreateAssetMenu(fileName = "YaConfig", menuName = "Yandex Game/Create config")]
    public class Config: ScriptableObject
    {
        [Header("Базовые настройки")]
        
        [Tooltip("Размер изображения пользователя")]
        public PlayerPhotoSizeEnum playerPhotoSize;
        [Tooltip("При инициализации объекта Player авторизованному игроку будет показано диалоговое окно с запросом на предоставление доступа к персональным данным." +
                 "Запрашивается доступ только к аватару и имени, идентификатор пользователя всегда передается автоматически. Примерное содержание: " +
                 "Игра запрашивает доступ к вашему аватару и имени пользователя на сервисах Яндекса.\n" +
                 "Если вам достаточно знать идентификатор, а имя и аватар пользователя не нужны, используйте опциональный параметр scopes: false. " +
                 "В этом случае диалоговое окно не будет показано.")]
        public bool isScopes = true;
        public PlayerInfoSimulationEntity playerInfoSimulation;
        
        [Tooltip("Если настройка включена, то информация о полной загрузке игры отправится в Яндекс. Если Ваш проект подгружает разную информацию после старта сцены, то отключите настройку и воспользуйтесь `YandexGame.GameReady`")]
        public bool isNeedAutoGameReadyStart = true;
        
        [Header("Доска лидеров")]
        
        [Tooltip("Включить модуль доски лидеров?")]
        public bool isLeaderboardEnable = true;
        [Tooltip("Записывать рекорд анонимного игрока?")]
        public bool isNeedSaveScoreAnonymousPlayer = true;
        [Tooltip("Использовать кастомный спрайт для отображения пользователей без аватаров.")]
        public Sprite customHiddenPlayerPhoto;
        [Tooltip("Кол-во получения верхних топ игроков")]
        [Range(1, 20)]
        public int quantityTop = 5;

        [Tooltip("Кол-во получаемых записей возле пользователя")]
        [Range(1, 10)]
        public int quantityAround = 2;
        
        [Header("Другие настройки")]
        [Tooltip("Включить автоматическую архивацию билда?")]
        public bool isNeedArchivingBuild = true;
        public bool isDebug = true;
        
        public string GetPlayerPhotoSize()
        {
            return playerPhotoSize switch
            {
                PlayerPhotoSizeEnum.Small => "small",
                PlayerPhotoSizeEnum.Medium => "medium",
                PlayerPhotoSizeEnum.Large => "large",
                PlayerPhotoSizeEnum.NonePhoto => "nonePhoto",
                _ => null
            };
        }
    }
}