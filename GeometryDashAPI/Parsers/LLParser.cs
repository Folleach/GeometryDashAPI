using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Parsers
{
    public class LLParser
    {
        private readonly string sense;

        public LLParser(string sense)
        {
            this.sense = sense;
        }

        public IEnumerable<string> Parse(string value)
        {
            var builder = new StringBuilder();
            var index = 0;
            while (index < value.Length)
            {
                if (IsSense(value, index))
                {
                    yield return builder.ToString();
                    index++;
                    builder.Clear();
                    continue;
                }
                index = Write(builder, value, index);
            }
            if (builder.Length > 0)
                yield return builder.ToString();
        }

        private int Write(StringBuilder builder, string value, int index)
        {
            builder.Append(value[index]);
            return ++index;
        }

        private bool IsSense(string value, int index)
        {
            for (var i = 0; i < sense.Length && index + i < value.Length; i++)
            {
                if (value[index + i] != sense[i])
                    return false;
            }

            return true;
        }
    }
}