namespace GeometryDashAPI.Server.Dtos
{
    public class LevelDto : LevelPreviewDto
    {
        [GameProperty("4")] public string LevelString { get; set; }
        
        [GameProperty("28")] public string UploadDateTime { get; set; }
        [GameProperty("29")] public string SecondDateTime { get; set; }

        public override string GetParserSense() => ":";
    }
}