using System;
using System.Text;

namespace GeometryDashAPI.Parsers
{
    public class KeyValueSLLParser
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        private char sense;
        private StringBuilder builder = new StringBuilder();
        private int index = 0;
        private string value;

        public KeyValueSLLParser(char sense, string value = "")
        {
            if (value == null)
                throw new ArgumentException("Value can't be null");
            this.sense = sense;
            this.value = value;
        }

        public void SetValue(string value)
        {
            if (value == null)
                throw new ArgumentException("Value can't be null");
            this.value = value;
            index = 0;
        }

        // TODO: Need to optimize structure
        public bool Next()
        {
            byte state = 0;
            builder.Clear();
            while (index < value.Length)
            {
                if (value[index] == sense)
                {
                    ++state;
                    ++index;
                    if (state == 1)
                        Key = builder.ToString();
                    else if (state == 2)
                    {
                        Value = builder.ToString();
                        return true;
                    }
                    builder.Clear();
                    continue;
                }
                index = Write(builder, value, index);
            }
            if (builder.Length > 0 && state == 2)
            {
                Value = builder.ToString();
                return true;
            }
            return false;
        }

        private int Write(StringBuilder builder, string value, int index)
        {
            builder.Append(value[index]);
            return ++index;
        }
    }
}
