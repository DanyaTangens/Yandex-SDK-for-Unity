using System.Runtime.InteropServices;
using UnityEngine;
using YandexSDK.Entity;

namespace YandexSDK
{
    /**
     * Чтобы модульно разделять логику и не писать все в один файл, приходжится нарушать пространство имен и
     * название файла с названием класса
     */
    public partial class YandexGame
    {
        private PlayerDataEntity playerData;
        
        private string _playerId;
        public string playerId { get => _playerId; }

        public string playerName = "unauthorized";
        public string playerPhoto;
        public string photoSize;
        public string payingStatus;

        AuthorizationEntity authorizationEntity = new AuthorizationEntity();

        [YaInit]
        public static void InitializationGame()
        {
            Instance.photoSize = Instance.Config.GetPlayerPhotoSize();
#if !UNITY_EDITOR
            Debug.Log("Init Auth inGame");
            string playerData = InitPlayerInternal();
            Instance.SetInitializationSDK(playerData);
#else
            InitPlayerForEditor();
#endif
        }

#if UNITY_EDITOR
        private static void InitPlayerForEditor()
        {
            var auth = true;
            var name = Instance.Config.playerInfoSimulation.name;

            if (!Instance.Config.playerInfoSimulation.authorized)
            {
                auth = false;
                name = null;
            }
            else
            {
                if (!Instance.Config.isScopes)
                    name = "anonymous";
            }

            var playerDataSimulation = new AuthorizationEntity()
            {
                isPlayerAuth = auth,
                playerName = name,
                playerId = Instance.Config.playerInfoSimulation.uniqueID,
                playerPhoto = Instance.Config.playerInfoSimulation.photo,
                payingStatus = Instance.Config.playerInfoSimulation.payingStatus
            };

            var json = JsonUtility.ToJson(playerDataSimulation);
            Instance.SetInitializationSDK(json);
        }
#endif
        
        public static void RequestAuth(bool isNeedSendBack = true)
        {
#if !UNITY_EDITOR
            RequestAuthInternal(isNeedSendBack);
#else
            InitPlayerForEditor();
#endif
        }
        

        private void SetInitializationSDK(string data)
        {
            if (data is "noData" or "" or null)
            {
                _playerId = null;
                playerName = null;
                playerPhoto = null;
                payingStatus = null;

                onRejectedAuthorization.Invoke();
                Debug.LogError("Failed init player data");
                GetDataInvoke();
                return;
            }

            authorizationEntity = JsonUtility.FromJson<AuthorizationEntity>(data);

            if (authorizationEntity.isPlayerAuth)
            {
                isAuth = true;
                onResolvedAuthorization.Invoke();
            }
            else
            {
                isAuth = false;
                onRejectedAuthorization.Invoke();
            }

            _playerId = authorizationEntity.playerId;
            playerName = authorizationEntity.playerName;
            playerPhoto = authorizationEntity.playerPhoto;
            payingStatus = authorizationEntity.payingStatus;

            Message("Authorization - " + isAuth);
            GetDataInvoke();
        }

        private void AuthDialog()
        {
            Message(isAuth
                ? "Open Auth Dialog"
                : "SDK Яндекс Игр предлагает войти в аккаунт только тем пользователям, которые еще не вошли."
            );

#if !UNITY_EDITOR
            OpenAuthDialogInternal();
#endif
        }
        
        [DllImport("__Internal")]
        private static extern string InitPlayerInternal();
        
        [DllImport("__Internal")]
        public static extern void RequestAuthInternal(bool isNeedSendBack);
        
        [DllImport("__Internal")]
        private static extern void OpenAuthDialogInternal();
    }
}