using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeometryDashAPI.Parser
{
    public class GameTypeDescription
    {
        public Dictionary<string, PropertyWithAttribute<GamePropertyAttribute>> Properties
            = new Dictionary<string, PropertyWithAttribute<GamePropertyAttribute>>();

        public GameTypeDescription(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttributes<GamePropertyAttribute>().FirstOrDefault();
                if (attribute == null)
                    continue;
                AddProperty(property, attribute);
            }
        }

        private void AddProperty(PropertyInfo property, GamePropertyAttribute attribute)
        {
            Properties.Add(attribute.Key, new PropertyWithAttribute<GamePropertyAttribute>(property, attribute));
        }
    }
}