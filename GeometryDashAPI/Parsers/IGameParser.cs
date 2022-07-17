using System;
using System.Collections.Generic;

namespace GeometryDashAPI.Parsers
{
    public interface IGameParser
    {
        delegate T Parser<out T>(ReadOnlySpan<char> data);

        T Decode<T>(ReadOnlySpan<char> raw) where T : IGameObject;

        List<T> DecodeList<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject;
        T[] GetArray<T>(ReadOnlySpan<char> raw, string separator, Parser<T> getValue);

        IGameObject DecodeBlock(ReadOnlySpan<char> raw);
    }
}
