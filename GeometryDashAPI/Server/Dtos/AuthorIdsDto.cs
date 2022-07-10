using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    [AsStruct]
    public class AuthorIdsDto : GameObject
    {
        [GameProperty("0")] public int UserId { get; set; }
        [GameProperty("1")] public string UserName { get; set; }
        [GameProperty("2")] public int AccountId { get; set; }
        
        public override string GetParserSense() => ":";
    }
}