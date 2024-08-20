using System;
using UnityEngine;

namespace YandexSDK.Entity
{
    [Serializable]
    public class PlayerInfoSimulationEntity
    {
        public const string avatarExample = "https://justplaygames.ru/public/icon_player.png";
        
        public bool authorized = true;
        public bool isMobile;
        public string language = "ru";
        public string name = "Player this";
        public string uniqueID = "000";
        public string photo = avatarExample;
        [Tooltip("Четыре возможных значения, зависящих от частоты и объема покупок пользователя:\n\n •  paying — пользователь купил портальную валюту на сумму более 500 рублей за последний месяц.\n\n •  partially_paying — у пользователя была хотя бы одна покупка портальной валюты реальными деньгами за последний год.\n\n •  not_paying — пользователь не делал покупок портальной валюты реальными деньгами за последний год.\n\n •  unknown — пользователь не из РФ или он не разрешил передачу такой информации разработчику.")]
        public string payingStatus = "paying";
        [Tooltip("Время в мс, синхронизированное с сервером.")]
        public long serverTime = 1721201231000;
    }
}