using System;

namespace GeometryDashAPI.Parsers
{
    public interface IDescriptor<out T, in TKey>
    {
        T Create();
        void Set(IGameObject instance, int key, ReadOnlySpan<char> raw);
    }
}
