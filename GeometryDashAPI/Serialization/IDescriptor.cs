using System;
using System.Text;

namespace GeometryDashAPI.Serialization
{
    public interface IDescriptor<out T>
    {
        T Create();
        T Create(ReadOnlySpan<char> raw);
    }

    public interface ICopyDescriptor<in T>
    {
        void CopyTo(T instance, StringBuilder destination);
    }
}
