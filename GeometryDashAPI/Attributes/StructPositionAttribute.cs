using System;

namespace GeometryDashAPI.Attributes
{
    public class StructPositionAttribute : Attribute
    {
        public readonly int Position;

        public StructPositionAttribute(int position)
        {
            Position = position;
        }
    }
}