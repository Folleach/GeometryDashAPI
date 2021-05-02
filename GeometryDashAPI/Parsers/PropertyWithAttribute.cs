using System.Reflection;

namespace GeometryDashAPI
{
    public class PropertyWithAttribute<TAttribute>
    {
        public PropertyInfo Property;
        public TAttribute Attribute;

        public PropertyWithAttribute(PropertyInfo property, TAttribute attribute)
        {
            Property = property;
            Attribute = attribute;
        }
    }
}