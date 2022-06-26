using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class UserResponse : GameStruct
    {
        [StructPosition(0)]
        public UserInfoDto User { get; set; }
        
        [StructPosition(1)] public Pagination Page { get; set; }
        
        public override string GetParserSense() => "#";
    }
}