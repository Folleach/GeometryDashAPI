namespace GeometryDashAPI.Server.Dtos
{
    public class LevelDto : LevelPreviewDto
    {
        [GameProperty("4")] public string LevelString { get; set; }

        public override string GetParserSense() => ":";
    }
}