using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using csFastFloat;
using Microsoft.AspNetCore.WebUtilities;

namespace GeometryDashAPI
{
    public static class GameConvert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BoolToString(bool value, bool isReverse = false)
        {
            if (isReverse)
                return value ? "0" : "1";
            return value ? "1" : "0";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StringToBool(ReadOnlySpan<char> value, bool isReverse = false)
        {
            if (isReverse)
                return value.CompareTo("0", StringComparison.Ordinal) == 0;
            return value.CompareTo("1", StringComparison.Ordinal) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StringToBool(string value, bool isReverse = false)
        {
            if (isReverse)
                return value == "0";
            return value == "1";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SingleToString(float value)
        {
            return value.ToString(Culture.FormatProvider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float StringToSingle(ReadOnlySpan<char> value)
        {
            return FastFloatParser.ParseFloat(value, NumberStyles.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string DoubleToString(double value)
        {
            return value.ToString(Culture.FormatProvider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double StringToDouble(ReadOnlySpan<char> value)
        {
            return FastDoubleParser.ParseDouble(value, NumberStyles.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToBase64(byte[] value)
        {
            return WebEncoders.Base64UrlEncode(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] FromBase64(string base64)
        {
            if (base64 == null)
                return null;
            var wrongIndex = base64.IndexOf(' ', StringComparison.Ordinal);
            return WebEncoders.Base64UrlDecode(base64, 0, wrongIndex >= 0 ? wrongIndex : base64.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToBase64String(string value)
        {
            return WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FromBase64String(string base64)
        {
            if (base64 == null)
                return null;
            var wrongIndex = base64.IndexOf(' ', StringComparison.Ordinal);
            return Encoding.ASCII.GetString(WebEncoders.Base64UrlDecode(base64, 0, wrongIndex >= 0 ? wrongIndex : base64.Length));
        }
    }
}
