using System;

namespace GeometryDashAPI.Parsers
{
    public interface IDescriptor<out T, in TKey>
    {
        T Create();
        T Create(ReadOnlySpan<char> raw);
        void Set(IGameObject instance, int key, ReadOnlySpan<char> raw);
    }
}
