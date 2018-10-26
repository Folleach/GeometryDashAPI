using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using System.Collections.Generic;

namespace GeometryDashAPI.Levels
{
    public class ColorList
    {
        protected Dictionary<short, Color> Colors = new Dictionary<short, Color>();

        public Color this[short id]
        {
            get
            {
                if (!Colors.ContainsKey(id))
                    Colors.Add(id, new Color(id));
                return Colors[id];
            }
            set
            {
                if (value.ID != id)
                    value.ID = id;
                Colors[id] = value;
            }
        }
        public Color this[ColorType type]
        {
            get => this[(short)type];
            set => this[(short)type] = value;
        }

        public void SetID(short target, short to)
        {
            if (!Colors.ContainsKey(target))
                throw new UnableSetException($"Unable set. Target color '{target}' not found.");
            if (Colors.ContainsKey(to))
                throw new UnableSetException($"Unable set. Color '{to}' exists.");
            Color col = Colors[target];
            Colors.Remove(target);
            col.ID = to;
            Colors.Add(to, col);
        }
        public void SetID(ColorType target, ColorType to)
        {
            SetID((short)target, (short)to);
        }

        public void Remove(short id)
        {
            Colors.Remove(id);
        }
        public void Remove(ColorType type)
        {
            Remove((short)type);
        }

        public short AddColor(Color color)
        {
            if (Colors.ContainsKey(color.ID))
                Colors[color.ID] = color;
            else
                Colors.Add(color.ID, color);

            return color.ID;
        }

        public List<Color> ToList()
        {
            List<Color> list = new List<Color>();
            foreach (var element in Colors)
                list.Add(element.Value);
            return list;
        }
    }
}
