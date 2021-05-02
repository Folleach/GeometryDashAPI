using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeometryDashAPI.Parser
{
    public class GameTypeDescription
    {
        public Dictionary<string, AttributeMember<GamePropertyAttribute>> Members
            = new Dictionary<string, AttributeMember<GamePropertyAttribute>>();

        public GameTypeDescription(Type type)
        {
            foreach (var member in GetPropertiesAndFields(type))
            {
                var attribute = member.GetCustomAttributes<GamePropertyAttribute>().FirstOrDefault();
                if (attribute == null)
                    continue;
                AddProperty(member, attribute);
            }
        }

        private void AddProperty(MemberInfo property, GamePropertyAttribute attribute)
        {
            Members.Add(attribute.Key, new AttributeMember<GamePropertyAttribute>(property, attribute));
        }

        private IEnumerable<MemberInfo> GetPropertiesAndFields(Type type)
        {
            foreach (var property in type.GetProperties())
                yield return property;
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                yield return field;
        }
    }
}