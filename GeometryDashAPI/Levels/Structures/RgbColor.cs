using System;
using System.Globalization;

namespace GeometryDashAPI.Levels.Structures
{
    public struct RgbColor
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public RgbColor()
        {
        }

        public RgbColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString() => $"#{ToHex(this)}";

        public static string ToHex(RgbColor rgb) => $"{rgb.Red:X2}{rgb.Green:X2}{rgb.Blue:X2}";

        public static bool TryFromHex(ReadOnlySpan<char> hex, out RgbColor rgb)
        {
            rgb = new RgbColor();
            if (hex.Length != 6 && hex.Length != 7)
                return false;

            var baseIndex = hex[0] == '#' ? 1 : 0;
            if (!byte.TryParse(Slice(hex, baseIndex, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var r))
                return false;
            if (!byte.TryParse(Slice(hex, baseIndex + 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var g))
                return false;
            if (!byte.TryParse(Slice(hex, baseIndex + 4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var b))
                return false;
            rgb.Red = r;
            rgb.Green = g;
            rgb.Blue = b;
            return true;
        }

        public static RgbColor FromHex(ReadOnlySpan<char> hex)
        {
            if (TryFromHex(hex, out var rgb))
                return rgb;
            throw new ArgumentException($"color '{hex.ToString()}' isn't looking as hex");
        }
        
#if NETSTANDARD2_1
        private static ReadOnlySpan<char> Slice(ReadOnlySpan<char> span, int start, int length) => span.Slice(start, length);
#else
        private static string Slice(ReadOnlySpan<char> span, int start, int length) => span.Slice(start, length).ToString();
#endif
    }
}
