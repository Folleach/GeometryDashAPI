using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class AccountInfoResponse : GameStruct, IServerResponseCode
    {
        [StructPosition(0)] public AccountDto Account { get; set; }
        
        public override string GetParserSense() => "||";
        public int ResponseCode { get; set; }
        
    }
}