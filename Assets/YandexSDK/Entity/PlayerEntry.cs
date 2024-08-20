namespace YandexSDK.Entity
{
    public class PlayerEntry
    {
        private string uniqueID;
        private int score;
        private string playerName;
        private string avatar;
        private bool isAuthPlayer;

        public PlayerEntry(string uniqueID, int score, string playerName, string avatar, bool isAuthPlayer)
        {
            this.uniqueID = uniqueID;
            this.score = score;
            this.playerName = playerName;
            this.avatar = avatar;
            this.isAuthPlayer = isAuthPlayer;
        }

        public string UniqueID => uniqueID;

        public int Score => score;

        public string PlayerName => playerName;

        public string Avatar => avatar;

        public bool IsAuthPlayer => isAuthPlayer;
    }   
}
