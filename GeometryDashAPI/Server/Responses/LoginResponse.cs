namespace GeometryDashAPI.Server.Responses
{
    public class LoginResponse : GameStruct
    {
        [StructPosition(0)] public int AccountId { get; set; }
        [StructPosition(1)] public int UserId { get; set; }
        
        public override string GetParserSense() => ",";
    }
}