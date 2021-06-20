using System;

namespace GeometryDashAPI.Parsers
{
    public struct LLParser
    {
        private readonly string sense;
        private readonly string value;
        private int index;

        public LLParser(string sense, string value)
        {
            this.sense = sense;
            this.value = value;
            index = 0;
        }

        public unsafe Span<char> Next()
        {
            var startIndex = index;
            fixed (char* pointer = value)
            {
                var current = pointer + index;
                while (index < value.Length)
                {
                    var isSense = true;
                    for (var i = 0; i < sense.Length && index + i < value.Length; i++)
                    {
                        if (value[index + i] == sense[i])
                            continue;
                        isSense = false;
                        break;
                    }

                    if (isSense)
                    {
                        var span = new Span<char>(current, index - startIndex);
                        index += sense.Length;
                        return span;
                    }

                    index++;
                }

                if (index != startIndex)
                    return new Span<char>(current, index - startIndex);
            }

            return null;
        }

        // private bool IsSense(string value, int index)
        // {
        //     for (var i = 0; i < sense.Length && index + i < value.Length; i++)
        //     {
        //         if (value[index + i] != sense[i])
        //             return false;
        //     }
        //
        //     return true;
        // }
    }
}