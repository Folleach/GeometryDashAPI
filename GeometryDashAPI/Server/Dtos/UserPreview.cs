using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    [Sense(":")]
    public class UserPreview : GameObject
    {
        [GameProperty("1")] public string Name { get; set; }
        [GameProperty("2")] public int UserId { get; set; }
        [GameProperty("3")] public int Starts { get; set; }
        [GameProperty("4")] public int Demons { get; set; }
        [GameProperty("6")] public string K6 { get; set; }
        [GameProperty("8")] public int CreatorPoints { get; set; }
        [GameProperty("9")] public int IconPreview { get; set; }
        [GameProperty("10")] public int Color1 { get; set; }
        [GameProperty("11")] public int Color2 { get; set; }
        [GameProperty("13")] public int SecretCoins { get; set; }
        [GameProperty("14")] public int IconType { get; set; }
        [GameProperty("15")] public int Special { get; set; }
        [GameProperty("16")] public int AccountId { get; set; }
        [GameProperty("17")] public int UserCoins { get; set; }
    }
}
