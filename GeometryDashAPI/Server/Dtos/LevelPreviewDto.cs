namespace GeometryDashAPI.Server.Dtos
{
    public class LevelPreviewDto : GameObject
    {
        [GameProperty("1")] public int Id { get; set; }
        [GameProperty("2")] public string Name { get; set; }
        [GameProperty("3")] private string description;
        public string Description
        {
            get => GameConvert.FromBase64S(description);
            set => description = GameConvert.ToBase64S(value);
        }
        [GameProperty("5")] public int Version { get; set; }
        [GameProperty("6")] public int AuthorUserId { get; set; }
        [GameProperty("10")] public int Downloads { get; set; }
        [GameProperty("14")] public int Likes { get; set; }
        [GameProperty("18")] public int Difficult { get; set; }
        [GameProperty("35")] public int MusicId { get; set; }
        [GameProperty("42")] public bool Epic { get; set; }
        
        public override string GetParserSense() => ":";
    }
}