using System;
using System.Text;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Server;

namespace GeometryDashAPI.Serialization
{
    // note: The names are dependent!
    // Used for TypeDescriptor
    internal class Printers
    {
        public static ReadOnlySpan<char> GetOrDefault_Boolean__(bool value) => GameConvert.BoolToString(value);
        public static ReadOnlySpan<char> GetOrDefault_Byte__(byte value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_SByte__(sbyte value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int16__(short value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int32__(int value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int32_Y(int? value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Int64__(long value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Double__(double value) => GameConvert.DoubleToString(value);
        public static ReadOnlySpan<char> GetOrDefault_Single__(float value) => GameConvert.SingleToString(value);
        public static ReadOnlySpan<char> GetOrDefault_String__(string value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_BlockGroup__(BlockGroup value) => value.ToString();
        public static ReadOnlySpan<char> GetOrDefault_Hsv__(Hsv value) => Hsv.Parse(value);
        public static ReadOnlySpan<char> GetOrDefault_Pagination__(Pagination data) => "todo: implement print pagination";
        public static ReadOnlySpan<char> GetOrDefault_Guidelines__(Guidelines value) => Guidelines.Parse(value);

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
