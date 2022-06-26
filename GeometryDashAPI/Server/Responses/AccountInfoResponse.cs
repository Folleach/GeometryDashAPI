using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class AccountInfoResponse : GameStruct
    {
        [StructPosition(0)] public AccountDto Account { get; set; }
        
        public override string GetParserSense() => "||";
    }
}