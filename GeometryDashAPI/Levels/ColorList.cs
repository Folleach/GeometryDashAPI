using System.Collections.Generic;

namespace GeometryDashAPI.Levels
{
    public class ColorList : Dictionary<short, Color>
    {
        public short AddColor(Color color)
        {
            if (ContainsKey(color.ID))
                base[color.ID] = color;
            else
                base.Add(color.ID, color);

            return color.ID;
        }
    }
}
