using System;
using System.Collections.Generic;

namespace GeometryDashAPI.Parsers
{
    public interface IGameParser
    {
        T Decode<T>(ReadOnlySpan<char> raw) where T : IGameObject;

        List<T> DecodeArray<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject;

        IGameObject DecodeBlock(ReadOnlySpan<char> raw);
    }
}
