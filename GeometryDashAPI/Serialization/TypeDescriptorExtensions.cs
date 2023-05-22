using System.Text;

namespace GeometryDashAPI.Serialization;

public static class TypeDescriptorExtensions
{
    public static string AsString<T>(this TypeDescriptor<T> descriptor, T value) where T : IGameObject
    {
        var builder = new StringBuilder();
        descriptor.CopyTo(value, builder);
        return builder.ToString();
    }
}
