using System;
using System.Runtime.CompilerServices;

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

    public ref struct LLParserSpan
    {
        private readonly ReadOnlySpan<char> sense;
        private readonly ReadOnlySpan<char> value;
        private readonly int valueLength;
        private readonly int senseLength;
        private int index;

        public LLParserSpan(ReadOnlySpan<char> sense, ReadOnlySpan<char> value)
        {
            this.sense = sense;
            this.value = value;
            index = 0;
            valueLength = value.Length;
            senseLength = sense.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Span<char> Next()
        {
            var startIndex = index;
            fixed (char* pointer = value)
            {
                var current = pointer + index;
                while (index < valueLength)
                {
                    var isSense = true;
                    for (var i = 0; i < senseLength && index + i < valueLength; i++)
                    {
                        if (value[index + i] == sense[i])
                            continue;
                        isSense = false;
                        break;
                    }

                    if (isSense)
                    {
                        var span = new Span<char>(current, index - startIndex);
                        index += senseLength;
                        return span;
                    }

                    index++;
                }

                if (index != startIndex)
                    return new Span<char>(current, index - startIndex);
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryParseNext(out Span<char> key, out Span<char> value)
        {
            key = Next();
            value = Next();
            if (key == null)
                return false;
            if (value == null)
                throw new Exception("Invalid raw data. Count of components in raw data is odd");
            return true;
        }
    }
}