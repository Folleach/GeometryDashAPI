using System;
using System.Linq;
using System.Numerics;

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

    public unsafe ref struct LLParserSpan
    {
        private static readonly int VectorCount = Vector<short>.Count;
        // private static readonly Vector<short> indices = new(Enumerable.Range(1, VectorCount).Select(num => (short)num).ToArray());

        private readonly Span<short> valueRaw;
        private readonly ReadOnlySpan<char> value;
        private readonly int valueLength;
        private readonly Vector<short> senseBase;
        private int index;

        public LLParserSpan(ReadOnlySpan<char> sense, ReadOnlySpan<char> value)
        {
            valueLength = value.Length;
            fixed (char* p = value)
                valueRaw = new Span<short>(p, valueLength);
            this.value = value;
            index = 0;
            senseBase = new Vector<short>((short)sense.GetPinnableReference());
        }

        public ReadOnlySpan<char> Next()
        {
            if (index >= valueLength)
                return null;
            var current = index;
            var localStorage = stackalloc short[VectorCount];
            var localSpan = new Span<short>(localStorage, VectorCount);
            while (current < valueLength)
            {
                var examine = valueLength - current;
                if (examine > VectorCount)
                    examine = VectorCount;
                valueRaw.Slice(index, examine).CopyTo(localSpan);
                var valueVec = new Vector<short>(localSpan);
                var equals = Vector.Equals(senseBase, valueVec);
                for (var i = 0; i < examine; i++)
                {
                    if (equals[i] != 0)
                    {
                        var t = index;
                        index = current + i + 1;
                        return value.Slice(t, t - current + i);
                    }
                }

                current += examine + 1;
            }

            var tx = index;
            index = current;
            return current > tx ? value.Slice(tx, current - tx - 1) : null;
        }
    }
}