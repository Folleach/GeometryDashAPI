using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("||")]
    public class TopResponse : GameObject
    {
        [GameProperty("0")]
        [ArraySeparator("|")]
        public List<Account> Users { get; set; }
    }
}