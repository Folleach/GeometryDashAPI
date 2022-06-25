using System;

namespace GeometryDashAPI.Levels
{
    public class Hsv
    {
        const char SEPARATOR = 'a';

        public float Hue { get; set; }
        public float Saturation { get; set; } = 1;
        public float Brightness { get; set; } = 1;
        public bool DeltaSaturation { get; set; }
        public bool DeltaBrightness { get; set; }

        public bool IsDefault => Hue == 0 && Saturation == 1f && Brightness == 1f && !DeltaSaturation && !DeltaBrightness;

        public static string Parse(Hsv hsv)
        {
            return
                $"{hsv.Hue}{SEPARATOR}" +
                $"{GameConvert.SingleToString(hsv.Saturation)}{SEPARATOR}" +
                $"{GameConvert.SingleToString(hsv.Brightness)}{SEPARATOR}" +
                $"{GameConvert.BoolToString(hsv.DeltaSaturation)}{SEPARATOR}" +
                $"{GameConvert.BoolToString(hsv.DeltaBrightness)}";
        }

        public static Hsv Parse(ReadOnlySpan<char> data) => Parse(data.ToString());

        public static Hsv Parse(string raw)
        {
            var result = new Hsv();
            var dataArray = raw.Split(SEPARATOR);
            result.Hue = GameConvert.StringToSingle(dataArray[0]);
            result.Saturation = GameConvert.StringToSingle(dataArray[1]);
            result.Brightness = GameConvert.StringToSingle(dataArray[2]);
            result.DeltaSaturation = GameConvert.StringToBool(dataArray[3]);
            result.DeltaBrightness = GameConvert.StringToBool(dataArray[4]);

            return result;
        }
    }
}
