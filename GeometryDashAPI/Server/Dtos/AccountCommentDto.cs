using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    [Sense("~")]
    public class AccountCommentDto : GameObject
    {
        [GameProperty("2")] private string comment;
        public string Comment
        {
            get => GameConvert.FromBase64String(comment);
            set => comment = GameConvert.ToBase64String(value);
        }
        [GameProperty("4")] public int Likes { get; set; }
        [GameProperty("6")] public int Id { get; set; }
        [GameProperty("9")] public string Date { get; set; }
    }
}