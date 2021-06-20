using System.Collections.Generic;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class TopResponse : GameStruct, IServerResponseCode
    {
        [StructPosition(0)]
        [ArraySeparator("|")]
        public List<AccountDto> Users { get; set; }
        
        public override string GetParserSense() => "||";
        public int ResponseCode { get; set; }
    }
}