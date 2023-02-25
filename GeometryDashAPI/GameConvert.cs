using System;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace GeometryDashAPI
{
    public static class GameConvert
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
                return value == "0";
            return value == "1";
        }

        public static string SingleToString(float value)
        {
            return string.Format(Culture.FormatProvider, "{0}", value);
        }

        public static float StringToSingle(string value)
        {
            return float.Parse(value, NumberStyles.Any, Culture.FormatProvider);
        }
        
        public static string DoubleToString(double value)
        {
            return string.Format(Culture.FormatProvider, "{0}", value);
        }

        public static double StringToDouble(string value)
        {
            return double.Parse(value, NumberStyles.Any, Culture.FormatProvider);
        }

        public static string ToBase64(byte[] value)
        {
            return WebEncoders.Base64UrlEncode(value);
        }

        public static byte[] FromBase64(string base64)
        {
            if (base64 == null)
                return null;
            var wrongIndex = base64.IndexOf(" ", StringComparison.Ordinal);
            return WebEncoders.Base64UrlDecode(base64, 0, wrongIndex >= 0 ? wrongIndex : base64.Length);
        }
        
        public static string ToBase64String(string value)
        {
            return WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes(value));
        }

        public static string FromBase64String(string base64)
        {
            if (base64 == null)
                return null;
            var wrongIndex = base64.IndexOf(" ", StringComparison.Ordinal);
            return Encoding.ASCII.GetString(WebEncoders.Base64UrlDecode(base64, 0, wrongIndex >= 0 ? wrongIndex : base64.Length));
        }
    }
}
