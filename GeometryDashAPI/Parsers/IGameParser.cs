using System;
using System.Collections.Generic;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Parsers
{
    public interface IGameParser
    {
        T Decode<T>(string raw) where T : GameObject, new();
        T Decode<T>(ReadOnlySpan<char> raw) where T : GameObject, new();
        string Encode<T>(T obj) where T : GameObject;

        List<T> DecodeArray<T>(ReadOnlySpan<char> raw, string separator) where T : IGameObject;

        IGameObject DecodeBlock(string raw);
        IGameObject DecodeBlock(ReadOnlySpan<char> raw);
        string EncodeBlock(Block block);

        IGameObject Decode(Type type, string raw); // internal
        string Encode(Type type, GameObject obj); // internal
    }
}
