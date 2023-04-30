using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Serialization
{
    public interface IGameSerializer
    {
        delegate T Parser<out T>(ReadOnlySpan<char> data);

        T Decode<T>(ReadOnlySpan<char> raw) where T : IGameObject;
        ReadOnlySpan<char> Encode<T>(T value) where T : IGameObject;

        Action<IGameObject, StringBuilder> GetCopier(Type type);

        List<T> DecodeList<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject;
        T[] GetArray<T>(ReadOnlySpan<char> raw, string separator, Parser<T> getValue);

        IGameObject DecodeBlock(ReadOnlySpan<char> raw);
    }
}
