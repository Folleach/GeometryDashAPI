using System;
using System.Linq.Expressions;
using System.Text;
using GeometryDashAPI.Levels;
using Microsoft.Extensions.Primitives;

namespace GeometryDashAPI.Serialization
{
    // note: The names are dependent!
    // Used for TypeDescriptor
    internal class Printers
    {
        public static ReadOnlySpan<char> GetOrDefault_Boolean(bool value) => GameConvert.BoolToString(value);
        public static ReadOnlySpan<char> GetOrDefault_Byte(byte value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_SByte(sbyte value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int16(short value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int32(int value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int32_Y(int? value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int64(long value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Double(double value) => GameConvert.DoubleToString(value);
        public static ReadOnlySpan<char> GetOrDefault_Single(float value) => GameConvert.SingleToString(value);
        public static ReadOnlySpan<char> GetOrDefault_String(string value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_BlockGroup(BlockGroup value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Hsv(Hsv value) => Hsv.Parse(value);

        public delegate void PrinterAppend<in TInstance>(TInstance instance, StringBuilder destination);
        public static void PrintArray<T>(T[] array, string separator, StringBuilder destination, PrinterAppend<T> append)
        {
            var shouldIncludeSeparator = false;
            if (array == null)
                return;
            foreach (var item in array)
            {
                if (shouldIncludeSeparator)
                    destination.Append(separator);
                shouldIncludeSeparator = true;
                append(item, destination);
            }
        }
    }
}
