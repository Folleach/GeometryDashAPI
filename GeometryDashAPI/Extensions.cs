using System;
using System.Reflection;

namespace GeometryDashAPI
{
    public static class Extensions
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            MemberInfo info = value.GetType().GetMember(value.ToString())[0];
            object[] attributes = info.GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
