using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("#")]
    public class UserResponse : GameObject
    {
        [GameProperty("0")]
        public UserInfoDto User { get; set; }
        
        [GameProperty("1")] public Pagination Page { get; set; }
    }
}