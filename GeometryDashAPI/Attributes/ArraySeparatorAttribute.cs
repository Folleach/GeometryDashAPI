using System;

namespace GeometryDashAPI.Attributes
{
    public class ArraySeparatorAttribute : Attribute
    {
        public readonly string Separator;
        public bool SeparatorAtTheEnd { get; set; }

        public ArraySeparatorAttribute(string separator)
        {
            Separator = separator;
        }
    }
}
