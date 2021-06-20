namespace GeometryDashAPI.Server.Responses
{
    public class LoginResponse : GameStruct, IServerResponseCode
    {
        [StructPosition(0)] public int AccountId { get; set; }
        [StructPosition(1)] public int UserId { get; set; }
        
        public override string GetParserSense() => ",";

        public int ResponseCode { get; set; }
    }
}