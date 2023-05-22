using System.Reflection;
using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Serialization;

internal readonly struct SetterInfo<T>
{
    public readonly TypeDescriptorHelper.Setter<T> Setter;
    private readonly GamePropertyAttribute attribute;
    private readonly MemberInfo member;

    public SetterInfo(TypeDescriptorHelper.Setter<T> setter, GamePropertyAttribute attribute, MemberInfo member)
    {
        Setter = setter;
        this.attribute = attribute;
        this.member = member;
    }

    public override string ToString() => $"setter for: {attribute.Key} ({member.Name}) [{(member is PropertyInfo ? "property" : "field")}], from: {member.DeclaringType}";
}
