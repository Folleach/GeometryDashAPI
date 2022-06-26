using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    public class UserInfoDto : GameObject
    {
        [GameProperty("1")] public string Name { get; set; }
        [GameProperty("16")] public int AccountId { get; set; }

        public override string GetParserSense() => ":";
    }
}