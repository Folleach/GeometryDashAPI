using System;

namespace GeometryDashAPI
{
    public class ArraySeparatorAttribute : Attribute
    {
        public readonly string Separator;

        public ArraySeparatorAttribute(string separator)
        {
            Separator = separator;
        }
    }
}