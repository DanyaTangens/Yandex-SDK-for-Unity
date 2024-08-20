using System.Collections.Generic;

namespace YandexSDK.Entity
{
    public class PlayerEntries
    {
        private string leaderboardName;
        private List<PlayerEntry> playerEntries;

        public PlayerEntries(string leaderboardName, List<PlayerEntry> playerEntries)
        {
            this.leaderboardName = leaderboardName;
            this.playerEntries = playerEntries;
        }

        public string LeaderboardName => leaderboardName;

        public List<PlayerEntry> PlayerEntries1 => playerEntries;
    }
}