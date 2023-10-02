using System;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Server;

// ReSharper disable UnusedMember.Local

namespace GeometryDashAPI.Serialization
{
    // note: The names are dependent!
    // Used for TypeDescriptor
    // name pattern: <options>_<type>_<is nullable>
    // options: Behavior on boundary cases
    //   GetOrDefault, ...
    // type: Name of member type
    // is nullable:
    //   Y - is enable nullable
    //   _ - is disable
    internal class Parsers
    {
        public static bool GetOrDefault_Boolean__(ReadOnlySpan<char> data) => GameConvert.StringToBool(data);
#if NETSTANDARD2_1
        public static byte GetOrDefault_Byte__(ReadOnlySpan<char> data) => byte.TryParse(data, out var value) ? value : default;
        public static sbyte GetOrDefault_SByte__(ReadOnlySpan<char> data) => sbyte.TryParse(data, out var value) ? value : default;
        public static short GetOrDefault_Int16__(ReadOnlySpan<char> data) => short.TryParse(data, out var value) ? value : default;
        public static int GetOrDefault_Int32__(ReadOnlySpan<char> data) => int.TryParse(data, out var value) ? value : default;
        public static int? GetOrDefault_Int32_Y(ReadOnlySpan<char> data) => int.TryParse(data, out var value) ? value : default;
        public static long GetOrDefault_Int64__(ReadOnlySpan<char> data) => long.TryParse(data, out var value) ? value : default;
#else
        public static byte GetOrDefault_Byte__(ReadOnlySpan<char> data) => byte.TryParse(data.ToString(), out var value) ? value : default;
        public static sbyte GetOrDefault_SByte__(ReadOnlySpan<char> data) => sbyte.TryParse(data.ToString(), out var value) ? value : default;
        public static short GetOrDefault_Int16__(ReadOnlySpan<char> data) => short.TryParse(data.ToString(), out var value) ? value : default;
        public static int GetOrDefault_Int32__(ReadOnlySpan<char> data) => int.TryParse(data.ToString(), out var value) ? value : default;
        public static int? GetOrDefault_Int32_Y(ReadOnlySpan<char> data) => int.TryParse(data.ToString(), out var value) ? value : default;
        public static long GetOrDefault_Int64__(ReadOnlySpan<char> data) => long.TryParse(data.ToString(), out var value) ? value : default;
#endif
        public static double GetOrDefault_Double__(ReadOnlySpan<char> data) => GameConvert.StringToDouble(data);
        public static float GetOrDefault_Single__(ReadOnlySpan<char> data) => GameConvert.StringToSingle(data);
        public static string GetOrDefault_String__(ReadOnlySpan<char> data) => data.ToString();
        public static BlockGroup GetOrDefault_BlockGroup__(ReadOnlySpan<char> data) => BlockGroup.Parse(data);
        public static Hsv GetOrDefault_Hsv__(ReadOnlySpan<char> data) => Hsv.Parse(data);
        public static Pagination GetOrDefault_Pagination__(ReadOnlySpan<char> data) => Pagination.Parse(data);
        public static Guidelines GetOrDefault_Guidelines__(ReadOnlySpan<char> data) => Guidelines.Parse(data);
    }
}
