using System;

namespace GeometryDashAPI.Parsers
{
    public class ArrayParser
    {
        public static object Decode(Type type, string separator, string raw)
        {
            var instance = Activator.CreateInstance(type);
            var argument = type.GetGenericArguments()[0];
            var parser = new LLParser(separator, raw);

            Span<char> next = null;
            var add = type.GetMethod("Add");
            while ((next = parser.Next()) != null)
                add!.Invoke(instance, new object[] { GeometryDashApi.GetStringParser(argument)(next.ToString()) });
            return instance;
        }
    }
}