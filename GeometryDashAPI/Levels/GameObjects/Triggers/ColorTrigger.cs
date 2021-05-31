using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(899)]
    public class ColorTrigger : Trigger
    {
        [GameProperty("23", 0, true)] public short ColorId { get; set; }
        [GameProperty("7", (byte)255, true)] public byte Red { get; set; } = 255;
        [GameProperty("8", (byte)255, true)] public byte Green { get; set; } = 255;
        [GameProperty("9", (byte)255, true)] public byte Blue { get; set; } = 255;

        [GameProperty("10", 0.5f, true)] public float FadeTime { get; set; } = 0.5f;

        [GameProperty("15", false)] public bool PlayerColor1 { get; set; }
        [GameProperty("16", false)] public bool PlayerColor2 { get; set; }
        [GameProperty("17", false)] public bool Blending { get; set; }
        [GameProperty("35", 1f, true)] public float Opacity { get; set; } = 1f;
        [GameProperty("60", 1f, true)] public bool CopyOpacity { get; set; }

        [GameProperty("49")] public Hsv ColorHsv { get; set; }
        [GameProperty("50", 0)] public int TargetChannelId { get; set; }

        public ColorTrigger() : base(899)
        {
            IsTrigger = true;
        }
    }
}
