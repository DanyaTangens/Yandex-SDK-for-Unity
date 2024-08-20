using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using YandexSDK.Entity;
using PlayerEntry = YandexSDK.Entity.PlayerEntry;
using PlayerEntriesJson = YandexSDK.Leaderboard.Entity.PlayerEntries;

namespace YandexSDK
{
    public partial class YandexGame
    {
        private Dictionary<string, PlayerEntries> leaderboardPlayerEntriesMap;
        
        public Action<PlayerEntries> onGetLeaderboard;
        
        public void GetLeaderboard(string leaderboardName, int quantityTop, int quantityAround)
        {
#if !UNITY_EDITOR
           if (Instance.Config.isLeaderboardEnable)
            {
                Message("Get Leaderboard");
                if (leaderboardPlayerEntriesMap.ContainsKey(leaderboardName))
                {
                    onGetLeaderboard?.Invoke(leaderboardPlayerEntriesMap[leaderboardName]);
                    return;
                }

                GetLeaderboardScoresInternal(
                    leaderboardName,
                    Instance.Config.quantityTop,
                    Instance.Config.quantityAround,
                    Instance.Config.GetPlayerPhotoSize(),
                    IsAuth
                );
            }
            
#else
            Message("Get Leaderboard Simulation: " + leaderboardName);
#endif
        }
        
        public void SetLeaderboardScore(string leaderboardName, long score)
        {
            score = (long)Mathf.Clamp(score, 0, 9007199254740991);

            if (Config.isLeaderboardEnable && isAuth)
            {
                if (Instance.Config.isNeedSaveScoreAnonymousPlayer == false && playerName == "anonymous")
                    return;

#if !UNITY_EDITOR
                Message("New Leaderboard Record: " + score);
                SetLeaderboardScoreInternal(leaderboardName, (int)score);
#else
                Message($"Leaderboard: '{leaderboardName}'. Record: {score}");
#endif
            }
        }
        
        /**
         * Callback from client leaderboard.js
         */
        public void LeaderboardEntries(string data)
        {
            var playerEntriesRaw = JsonUtility.FromJson<PlayerEntriesJson>(data);

            var players = new List<PlayerEntry>();
            
            foreach (var playerEntry in playerEntriesRaw.playerEntries)
            {
                players.Add(new PlayerEntry(
                    playerEntry.uniqueID,
                    playerEntry.score,
                    playerEntry.playerName,
                    playerEntry.avatar,
                    playerEntry.uniqueID == playerId
                ));
            }

            var playerEntries = new PlayerEntries(playerEntriesRaw.leaderboardName, players);
            leaderboardPlayerEntriesMap.Add(playerEntriesRaw.leaderboardName, playerEntries);
            
            onGetLeaderboard?.Invoke(playerEntries);
        }
        
        [DllImport("__Internal")]
        private static extern void SetLeaderboardScoreInternal(string leaderboardName, int score);
        [DllImport("__Internal")]
        private static extern void GetLeaderboardScoresInternal(string leaderboardName, int quantityTop, int quantityAround, string photoSize, bool isAuth);
    }
}