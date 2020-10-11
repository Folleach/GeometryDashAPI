using System.Collections.Generic;
using System.Reflection;

namespace GeometryDashAPI.Parsers
{
    public class PropertyMapping
    {
        private Dictionary<PropertyInfo, GamePropertyAttribute> attributes = new Dictionary<PropertyInfo, GamePropertyAttribute>();
        private Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();

        public PropertyMapping()
        {

        }

        public void Register(PropertyInfo property, GamePropertyAttribute attribute)
        {
            attributes.Add(property, attribute);
            properties.Add(attribute.Key, property);
        }

        public PropertyInfo GetProperty(string id) => properties[id];
        public GamePropertyAttribute GetAttribute(PropertyInfo property) => attributes[property];
    }
}
