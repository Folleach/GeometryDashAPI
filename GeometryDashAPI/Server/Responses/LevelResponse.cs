using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("#")]
    [AsStruct]
    public class LevelResponse : GameObject
    {
        [GameProperty("0")] public LevelDto Level { get; set; }
        [GameProperty("1")] public string Hash1 { get; set; }
        [GameProperty("2")] public string Hash2 { get; set; }

        public override string GetParserSense() => "#";
    }
}