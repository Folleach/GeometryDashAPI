using System;
using System.Text;

namespace GeometryDashAPI.Serialization
{
    public interface IDescriptor<out T> where T : IGameObject
    {
        T Create();
        T Create(ReadOnlySpan<char> raw);
    }

    public interface ICopyDescriptor<in T> where T : IGameObject
    {
        void CopyTo(T instance, StringBuilder destination);
    }
}
