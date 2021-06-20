namespace GeometryDashAPI.Server.Dtos
{
    public class AccountDto : GameObject
    {
        [GameProperty("1")] public string Name { get; set; }
        [GameProperty("2")] public int UserId { get; set; }
        [GameProperty("3")] public int Starts { get; set; }
        [GameProperty("4")] public int Demons { get; set; }
        [GameProperty("8")] public int CreatorPoints { get; set; }
        [GameProperty("10")] public int Color1 { get; set; }
        [GameProperty("11")] public int Color2 { get; set; }
        [GameProperty("20")] public string YouTubeId { get; set; }
        [GameProperty("21")] public int CubeId { get; set; }
        [GameProperty("22")] public int ShipId { get; set; }
        [GameProperty("44")] public string TwitterId { get; set; }
        [GameProperty("45")] public string TwitchId { get; set; }
        
        public override string GetParserSense() => ":";
    }
}