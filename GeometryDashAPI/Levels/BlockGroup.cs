using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Levels
{
    public class BlockGroup : List<int>
    {
        public bool IsEmpty => Count == 0;

        public override string ToString()
        {
            var builder = new StringBuilder();
            var first = true;
            foreach (var id in this)
            {
                if (first)
                {
                    builder.Append(id);
                    first = false;
                }
                else
                    builder.Append($".{id}");
            }
            return builder.ToString();
        }

        public static BlockGroup Parse(ReadOnlySpan<char> data)
        {
            var result = new BlockGroup();
            var parser = new LLParserSpan(".", data);
            while (true)
            {
                var idRaw = parser.Next();
                if (idRaw == null)
                    break;
                if (!int.TryParse(idRaw, out var id))
                    throw new Exception($"Can't parse group id in: {data.ToString()}");
                result.Add(id);
            }

            return result;
        }

        public static BlockGroup Parse(string data)
        {
            var result = new BlockGroup();
            var parser = new LLParserSpan(".", data);
            while (true)
            {
                var idRaw = parser.Next();
                if (idRaw == null)
                    break;
#if NETSTANDARD2_1
                if (!int.TryParse(idRaw, out var id))
#else
                if (!int.TryParse(idRaw.ToString(), out var id))
#endif
                    throw new Exception($"Can't parse group id in: {data}");
                result.Add(id);
            }

            return result;
        }
    }
}
