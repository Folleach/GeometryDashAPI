using System;
using System.Globalization;

namespace GeometryDashAPI
{
    public class GameConvert
    {
        public static string BoolToString(bool value, bool isReverse = false)
        {
            if (isReverse)
                return value ? "0" : "1";
            return value ? "1" : "0";
        }

        public static bool StringToBool(string value, bool isReverse = false)
        {
            if (isReverse)
                return value == "1" ? false : true;
            return value == "1" ? true : false;
        }

        public static string SingleToString(float value)
        {
            return value.ToString().Replace(',', '.');
        }

        public static float StringToSingle(string value)
        {
            return float.Parse(value, NumberStyles.Any, Culture.FormatProvider);
        }

        public static string ToBase64(byte[] data)
        {
            return Convert.ToBase64String(data).Replace("/", "_").Replace("+", "-");
        }

        public static byte[] FromBase64(string data)
        {
            return Convert.FromBase64String(data.Replace("_", "/").Replace("-", "+"));
        }
    }
}
