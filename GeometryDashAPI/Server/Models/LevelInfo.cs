namespace GeometryDashAPI.Server.Models
{
    public class LevelInfo : GameObject
    {
        [GameProperty("1")]
        public int Id { get; set; }
        [GameProperty("2")]
        public string Name { get; set; }
        [GameProperty("3")]
        public string Description { get; set; }
        [GameProperty("4")]
        public string Level { get; set; }

        public override string ParserSense { get; } = ":";
    }
}
