using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos
{
    public class LevelDto : LevelPreviewDto
    {
        [GameProperty("4")]
        public string LevelString { get; set; }

        [GameProperty("27")]
        private string rawPassword;
        public string RawPassword // Are you need decrypt? I can do it later. On the clock 12:25 am
        {
            get => rawPassword;
            set => rawPassword = value;
        }
        
        [GameProperty("28")]
        public string UploadDateTime { get; set; }
        
        [GameProperty("29")]
        public string SecondDateTime { get; set; }
        
        [GameProperty("36")]
        public string ExtraString { get; set; }
        
        [GameProperty("40")]
        public bool LowDetail { get; set; }

        public override string GetParserSense() => ":";
    }
}