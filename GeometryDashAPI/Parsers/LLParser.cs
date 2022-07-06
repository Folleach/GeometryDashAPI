using System;
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

    public ref struct LLParserSpan
    {
        private static readonly int VectorCount = Vector<short>.Count;

        private readonly ReadOnlySpan<char> sense;
        private readonly ReadOnlySpan<char> value;
        private readonly int senseLength;
        private readonly int valueLength;
        private readonly Vector<short> senseBase;
        private readonly int vecCount;
        private int index;

        public LLParserSpan(ReadOnlySpan<char> sense, ReadOnlySpan<char> value)
        {
            this.sense = sense;
            this.value = value;
            senseLength = sense.Length;
            valueLength = value.Length;
            index = 0;
            vecCount = Vector<short>.Count;
            var arr = new short[VectorCount];
            for (var i = 0; i < VectorCount; i++)
                arr[i] = (short)sense.GetPinnableReference();
            senseBase = new Vector<short>(arr);
        }
        
        public unsafe ReadOnlySpan<char> Next()
        {
            if (index >= valueLength)
                return null;
            fixed (char* p = value)
            {
                var current = index;
                while (current < valueLength)
                {
                    var examine = valueLength - current;
                    if (examine > VectorCount)
                        examine = VectorCount;
                    var valueVec = new Vector<short>(new Span<short>(p + index, VectorCount));
                    var equals = Vector.Equals(valueVec, senseBase);
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
        
        public unsafe ReadOnlySpan<char> NextFirstAttemptOfVectors()
        {
            if (index >= valueLength)
                return null;
            fixed (char* p = value)
            {
                var current = index;
                while (current < valueLength)
                {
                    var examine = valueLength - current;
                    if (examine > VectorCount)
                        examine = VectorCount;
                    var valueVec = new Vector<short>(new Span<short>(p + index, VectorCount));
                    var equals = Vector.Equals(valueVec, senseBase);
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

        public unsafe Span<char> NextOnSpanExample()
        {
            if (index >= valueLength)
                return null;
            var current = value.Slice(index);
            var next = current.IndexOf(sense, StringComparison.Ordinal);
            fixed (char* p = current)
            {
                var span = next == -1 ? new Span<char>(p, current.Length) : new Span<char>(p, next);
                index += next == -1 ? current.Length : next + 1;
                return span;
            }
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