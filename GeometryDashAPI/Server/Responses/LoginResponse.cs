using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Responses
{
    [Sense(",")]
    public class LoginResponse : GameObject
    {
        [GameProperty("0")] public int AccountId { get; set; }
        [GameProperty("1")] public int UserId { get; set; }
    }
}