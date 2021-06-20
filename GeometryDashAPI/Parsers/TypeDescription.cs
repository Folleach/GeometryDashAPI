using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeometryDashAPI.Parsers
{
    public class TypeDescription<TKey, TValue> where TValue : Attribute
    {
        private readonly Func<TValue, TKey> keySelector;
        public readonly Dictionary<TKey, MemberDescription<TValue>> Members = new();

        public TypeDescription(Type type, Func<TValue, TKey> keySelector)
        {
            this.keySelector = keySelector;
            foreach (var member in GetPropertiesAndFields(type))
            {
                var attribute = member.GetCustomAttributes<TValue>().FirstOrDefault();
                if (attribute == null)
                    continue;
                AddProperty(member, attribute);
            }
        }

        private void AddProperty(MemberInfo property, TValue attribute)
        {
            var key = keySelector(attribute);
            Members.Add(key, new MemberDescription<TValue>(property, attribute));
        }

        private static IEnumerable<MemberInfo> GetPropertiesAndFields(Type type)
        {
            foreach (var property in type.GetProperties())
                yield return property;
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                yield return field;
        }
    }
}