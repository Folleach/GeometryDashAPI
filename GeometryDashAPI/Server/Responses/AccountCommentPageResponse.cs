using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class AccountCommentPageResponse : GameStruct
    {
        [StructPosition(0)]
        [ArraySeparator("|")]
        public List<AccountCommentDto> Comments { get; set; }
        
        [StructPosition(1)]
        public Pagination Page { get; set; }
        
        public override string GetParserSense() => "#";
    }
}