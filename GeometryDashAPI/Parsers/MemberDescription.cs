using System;
using System.Collections.Generic;
using System.Reflection;
using GeometryDashAPI.Attributes;

namespace GeometryDashAPI
{
    public class MemberDescription<TAttribute>
    {
        public readonly MemberInfo Member;
        public readonly TAttribute Attribute;
        public readonly bool IsProperty;
        public readonly ArraySeparatorAttribute ArraySeparatorAttribute;
        
        public Type MemberType => IsProperty ? ((PropertyInfo) Member).PropertyType : ((FieldInfo) Member).FieldType;

        public MemberDescription(MemberInfo member, TAttribute attribute)
        {
            Member = member;
            IsProperty = member is PropertyInfo;
            Attribute = attribute;
            
            if (MemberType!.IsGenericType &&
                MemberType.GetGenericTypeDefinition() == typeof(List<>))
                ArraySeparatorAttribute = member.GetCustomAttribute<ArraySeparatorAttribute>();
        }

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