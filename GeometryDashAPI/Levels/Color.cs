using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Structures;
using System;

namespace GeometryDashAPI.Levels
{
    [Sense("_")]
    public class Color : GameObject
    {
        [GameProperty("1", 255, true, Order = 1)] public byte Red { get; set; } = 255;
        [GameProperty("2", 255, true, Order = 2)] public byte Green { get; set; } = 255;
        [GameProperty("3", 255, true, Order = 3)] public byte Blue { get; set; } = 255;
        [GameProperty("11", 255, Order = 4)] public byte Red2 { get; set; } = 255;
        [GameProperty("12", 255, Order = 5)] public byte Green2 { get; set; } = 255;
        [GameProperty("13", 255, Order = 6)] public byte Blue2 { get; set; } = 255;

        /// <summary>
        /// -1 - none, 0 - none, 1 - PlayerColor1, 2 - PlayerColor2
        /// </summary>
        [GameProperty("4", Order = 10)] public sbyte PlayerColor { get; set; }

        [GameProperty("6", 0, true, Order = 12)] public int Id { get; set; }
        [GameProperty("5", Order = 13)] public bool Blending { get; set; }
        [GameProperty("7", 1f, true, Order = 14)] public float Opacity { get; set; } = 1f;
        [GameProperty("15", 1, true, Order = 15)] public int K15 { get; set; } = 1;
        [GameProperty("18", 0, Order = 16)] public int K18 { get; set; }
        [GameProperty("8", 1, true, Order = 17)] public int K8 { get; set; } = 1;

        [GameProperty("17")] public bool CopyOpacity { get; set; }
        [GameProperty("10")] public Hsv ColorHSV { get; set; }
        [GameProperty("9")] public int TargetChannelID { get; set; }

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

        public string AsHex() => $"#{Red:X2}{Green:X2}{Blue:X2}";

        public override string ToString()
        {
            return $"(id = {Id}, color = {AsHex()})";
        }

        public static string RgbToHex(RgbColor rgb) => $"{rgb.Red:X2}{rgb.Green:X2}{rgb.Blue:X2}";
        public static RgbColor HexToRgb(ReadOnlySpan<char> hex) => new()
        {
            Red = (byte)((byte.Parse(hex.Slice(1, 2)) >> 16) & 0xFF),
            Green = (byte)((byte.Parse(hex.Slice(3, 2)) >> 8) & 0xFF),
            Blue = (byte)(byte.Parse(hex.Slice(5, 2)) & 0xFF)
        };
    }
}
