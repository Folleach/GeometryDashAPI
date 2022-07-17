using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels
{
    [Sense("_")]
    public class Color : GameObject
    {
        [GameProperty("6", 0, true)] public int Id { get; set; }
        [GameProperty("1", 255, true)] public byte Red { get; set; } = 255;
        [GameProperty("2", 255, true)] public byte Green { get; set; } = 255;
        [GameProperty("3", 255, true)] public byte Blue { get; set; } = 255;
        [GameProperty("11", 255, true)] public byte Red2 { get; set; } = 255;
        [GameProperty("12", 255, true)] public byte Green2 { get; set; } = 255;
        [GameProperty("13", 255, true)] public byte Blue2 { get; set; } = 255;

        /// <summary>
        /// -1 - none, 0 - none, 1 - PlayerColor1, 2 - PlayerColor2
        /// </summary>
        [GameProperty("4")] public sbyte PlayerColor { get; set; }
        [GameProperty("5")] public bool Blending { get; set; }
        [GameProperty("7", 1f, true)] public float Opacity { get; set; } = 1f;
        [GameProperty("17")] public bool CopyOpacity { get; set; }

        [GameProperty("10")] public Hsv ColorHSV { get; set; }
        [GameProperty("9")] public int TargetChannelID { get; set; }

        [GameProperty("15", 1, true)] public int K15 { get; set; } = 1;
        [GameProperty("18", 0, true)] public int K18 { get; set; }
        [GameProperty("8", 1, true)] public int K8 { get; set; } = 1;

        public Color()
        {
        }

        public Color(int id)
        {
            Id = id;
        }

        public void SetColor(byte r, byte g, byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }
    }
}
