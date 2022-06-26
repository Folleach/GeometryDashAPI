using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class TopResponse : GameStruct
    {
        [StructPosition(0)]
        [ArraySeparator("|")]
        public List<AccountDto> Users { get; set; }
        
        public override string GetParserSense() => "||";
    }
}