using System.Text;

namespace GeometryDashAPI.Server.Dtos
{
    public class LevelDto : GameObject
    {
        [GameProperty("1")]
        public int Id { get; set; }
        [GameProperty("2")]
        public string Name { get; set; }

        [GameProperty("3")]
        private string description;
        public string Description
        {
            get => Encoding.UTF8.GetString(GameConvert.FromBase64(description));
            set => description = GameConvert.ToBase64(Encoding.UTF8.GetBytes(value));
        }
        
        [GameProperty("4")]
        public string LevelString { get; set; }
        [GameProperty("10")]
        public int Downloads { get; set; }
        [GameProperty("14")]
        public int Likes { get; set; }
        [GameProperty("35")]
        public int SongId { get; set; }
        
        public override string GetParserSense() => ":";
    }
}