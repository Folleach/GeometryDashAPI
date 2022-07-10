using System;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Parsers
{
    public interface IGameParser
    {
        T Decode<T>(string raw) where T : GameObject, new();
        T Decode<T>(ReadOnlySpan<char> raw) where T : GameObject, new();
        string Encode<T>(T obj) where T : GameObject;

        Block DecodeBlock(string raw);
        Block DecodeBlock(ReadOnlySpan<char> raw);
        string EncodeBlock(Block block);

        GameObject Decode(Type type, string raw); // internal
        string Encode(Type type, GameObject obj); // internal
    }
}