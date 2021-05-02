using System;
using System.Reflection;

namespace GeometryDashAPI
{
    public class AttributeMember<TAttribute>
    {
        public MemberInfo Member;
        public TAttribute Attribute;
        public bool IsGameObject;
        public bool IsProperty;

        public AttributeMember(MemberInfo member, TAttribute attribute)
        {
            Member = member;
            IsProperty = member is PropertyInfo;
            Attribute = attribute;
            IsGameObject = typeof(GameObject).IsAssignableFrom(MemberType);
        }

        public Type MemberType => IsProperty ? ((PropertyInfo) Member).PropertyType : ((FieldInfo) Member).FieldType;

        public void SetValue(object instance, object value)
        {
            if (IsProperty)
            {
                ((PropertyInfo) Member).SetValue(instance, value);
                return;
            }
            ((FieldInfo) Member).SetValue(instance, value);
        }

        public object GetValue(object instance)
        {
            if (IsProperty)
                return ((PropertyInfo) Member).GetValue(instance);
            return ((FieldInfo) Member).GetValue(instance);
        }
    }
}