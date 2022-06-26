using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    public class AuthorIdsDto : GameStruct
    {
        [StructPosition(0)] public int UserId { get; set; }
        [StructPosition(1)] public string UserName { get; set; }
        [StructPosition(2)] public int AccountId { get; set; }
        
        public override string GetParserSense() => ":";
    }
}