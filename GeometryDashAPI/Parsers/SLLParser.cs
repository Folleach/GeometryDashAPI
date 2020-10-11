using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Parsers
{
    public class SLLParser
    {
        private readonly char sense;

        public SLLParser(char sense)
        {
            this.sense = sense;
        }

        public IEnumerable<string> Parse(string value)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            while (index < value.Length)
            {
                if (value[index] == sense)
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
    }
}
