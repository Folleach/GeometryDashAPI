using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Levels
{
    public class BlockGroup : List<int>
    {
        public bool IsEmpty => Count == 0;

        public override string ToString()
        {
            var builder = new StringBuilder();
            var first = true;
            foreach (var id in ToArray())
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

        public static BlockGroup Parse(string raw)
        {
            var result = new BlockGroup();
            var parser = new LLParser('.', raw);
            while (true)
            {
                var idRaw = parser.Next();
                if (idRaw == null)
                    break;
                if (!int.TryParse(idRaw, out var id))
                    throw new Exception($"Can't parse group id in: {raw}");
                result.Add(id);
            }

            return result;
        }
    }
}
