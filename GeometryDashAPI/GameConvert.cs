using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using csFastFloat;
using NeoSmart.Utils;

namespace GeometryDashAPI
{
    public static class GameConvert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BoolToString(bool value, bool isReverse = false)
        {
            return value ^ isReverse ? "1" : "0";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StringToBool(ReadOnlySpan<char> value, bool isReverse = false)
        {
            return (value.CompareTo("1".AsSpan(), StringComparison.Ordinal) == 0) ^ isReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool StringToBool(string value, bool isReverse = false)
        {
            return value == "1" ^ isReverse;
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
        public static string ToBase64(ReadOnlySpan<byte> value)
        {
            return UrlBase64.Encode(value, PaddingPolicy.Preserve);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] FromBase64(string base64)
        {
            if (base64 == null)
                return null;
            return UrlBase64.Decode(base64);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToBase64String(string value)
        {
            return UrlBase64.Encode(Encoding.ASCII.GetBytes(value), PaddingPolicy.Preserve);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FromBase64String(string base64)
        {
            if (base64 == null)
                return null;
            return Encoding.ASCII.GetString(UrlBase64.Decode(base64));
        }
    }
}
