using System.Reflection;

namespace GeometryDashAPI
{
    public class PropertyWithAttribute<TAttribute>
    {
        public PropertyInfo Property;
        public TAttribute Attribute;
        public bool IsGameObject;

        public PropertyWithAttribute(PropertyInfo property, TAttribute attribute)
        {
            Property = property;
            Attribute = attribute;
            IsGameObject = typeof(GameObject).IsAssignableFrom(property.PropertyType);
        }
    }
}