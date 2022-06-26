using System;

namespace GeometryDashAPI.Attributes
{
    public class SenseAttribute : Attribute
    {
        public readonly string Sense;

        public SenseAttribute(string sense)
        {
            Sense = sense;
        }
    }
}
