using System;
using System.Runtime.CompilerServices;

namespace GeometryDashAPI.Parsers
{
    // ReSharper disable once InconsistentNaming
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
                fixed (char* sensePointer = sense)
                {
                    var current = pointer + index;
                    while (index < valueLength)
                    {
                        var isSense = true;
                        for (var i = 0; i < senseLength && index + i < valueLength; i++)
                        {
                            if (*(pointer + index + i) == *(sensePointer + i))
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
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryParseNext(out ReadOnlySpan<char> key, out ReadOnlySpan<char> value)
        {
            key = Next();
            if (key == null)
            {
                value = null;
                return false;
            }
            value = Next();
            if (value == null)
                throw new Exception("Invalid raw data. Count of components in raw data is odd");
            return true;
        }
    }
}