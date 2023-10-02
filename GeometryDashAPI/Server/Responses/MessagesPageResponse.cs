using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses;

[AsStruct]
[Sense("#")]
public class MessagesPageResponse : GameObject
{
    [GameProperty("0")]
    [ArraySeparator("|")]
    public List<MessagePreview> Messages { get; set; }
    
    [GameProperty("1")]
    public Pagination Page { get; set; }
}
