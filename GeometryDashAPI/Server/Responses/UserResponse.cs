using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("#")]
    [AsStruct]
    public class UserResponse : GameObject
    {
        [GameProperty("0")]
        public UserPreview User { get; set; }
        
        [GameProperty("1")] public Pagination Page { get; set; }
    }
}
