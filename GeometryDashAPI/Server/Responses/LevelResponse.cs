using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class LevelResponse : GameStruct
    {
        [StructPosition(0)] public LevelDto Level { get; set; }
        [StructPosition(1)] public string Hash1 { get; set; }
        [StructPosition(2)] public string Hash2 { get; set; }

        public override string GetParserSense() => "#";
    }
}