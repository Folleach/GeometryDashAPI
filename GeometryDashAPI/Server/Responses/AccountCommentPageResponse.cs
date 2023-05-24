using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("#")]
    [AsStruct]
    public class AccountCommentPageResponse : GameObject
    {
        [GameProperty("0")]
        [ArraySeparator("|")]
        public List<AccountComment> Comments { get; set; }
        
        [GameProperty("1")]
        public Pagination Page { get; set; }
    }
}
