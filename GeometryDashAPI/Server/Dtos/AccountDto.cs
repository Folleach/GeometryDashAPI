namespace GeometryDashAPI.Server.Dtos
{
    public class AccountDto : GameObject
    {
        [GameProperty("1")] public string Name { get; set; }
        [GameProperty("2")] public int UserId { get; set; }
        [GameProperty("3")] public int Starts { get; set; }
        [GameProperty("4")] public int Demons { get; set; }
        [GameProperty("6")] public int Rank { get; set; }
        [GameProperty("7")] public int Highlight { get; }
        [GameProperty("8")] public int CreatorPoints { get; set; }
        [GameProperty("9")] public int IconPrev { get; set; }
        [GameProperty("10")] public int Color1 { get; set; }
        [GameProperty("11")] public int Color2 { get; set; }
        [GameProperty("13")] public int SecretCoins { get; set; }
        [GameProperty("14")] public int IconType { get; set; }
        [GameProperty("15")] public int Special { get; set; }
        [GameProperty("16")] public int AccountId { get; set; }
        [GameProperty("17")] public int UserCoins { get; set; }
        [GameProperty("18")] public int MessageState { get; set; }
        [GameProperty("19")] public int FriendsState { get; set; }
        [GameProperty("20")] public string YouTubeId { get; set; }
        [GameProperty("21")] public int CubeId { get; set; }
        [GameProperty("22")] public int ShipId { get; set; }
        [GameProperty("23")] public int BallId { get; set; }
        [GameProperty("24")] public int BirdId { get; set; }
        [GameProperty("25")] public int WaveId { get; set; }
        [GameProperty("26")] public int RobotId { get; set; }
        [GameProperty("27")] public int StreakId { get; set; }
        [GameProperty("28")] public int GlowId { get; set; }
        [GameProperty("29")] public bool IsRegistered { get; set; }
        [GameProperty("30")] public int GlobalRank { get; set; }
        [GameProperty("31")] public int UsFriendState { get; set; }
        [GameProperty("38")] public int Messages { get; set; }
        [GameProperty("39")] public int FriendRequests { get; set; }
        [GameProperty("40")] public bool NewFriends { get; set; }
        [GameProperty("41")] public int NewFriendRequests { get; set; }
        [GameProperty("42")] public int TimeSinceSubmittedLevel { get; set; }
        [GameProperty("43")] public int SpiderId { get; set; }
        [GameProperty("44")] public string TwitterId { get; set; }
        [GameProperty("45")] public string TwitchId { get; set; }
        [GameProperty("46")] public int Diamonds { get; set; }
        [GameProperty("48")] public int ExplosionId { get; set; }
        [GameProperty("49")] public int Moderator { get; set; }
        [GameProperty("50")] public int CommentHistoryState { get; set; }
        
        public override string GetParserSense() => ":";
    }
}