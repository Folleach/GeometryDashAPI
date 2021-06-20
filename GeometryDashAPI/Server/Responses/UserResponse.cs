using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    public class UserResponse : GameStruct, IServerResponseCode
    {
        [StructPosition(0)]
        public UserInfoDto User { get; set; }
        
        [StructPosition(1)] public Pagination Page { get; set; }
        
        public override string GetParserSense() => "#";
        
        public int ResponseCode { get; set; }
    }
}