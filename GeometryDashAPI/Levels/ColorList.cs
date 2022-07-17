using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using System.Collections.Generic;

namespace GeometryDashAPI.Levels
{
    public class ColorList : GameObject
    {
        private readonly Dictionary<int, Color> colors = new Dictionary<int, Color>();

        public Color this[int id]
        {
            get
            {
                if (!colors.TryGetValue(id, out var color))
                    colors.Add(id, color = new Color(id));
                return color;
            }
            set
            {
                if (value.Id != id)
                    value.Id = id;
                colors[id] = value;
            }
        }

        public Color this[ColorType type]
        {
            get => this[(int)type];
            set => this[(int)type] = value;
        }

        public int Count => colors.Count;

        public void SetId(int target, int to)
        {
            if (!colors.ContainsKey(target))
                throw new UnableSetException($"Unable set. Target color '{target}' not found.");
            if (colors.ContainsKey(to))
                throw new UnableSetException($"Unable set. Color '{to}' exists.");
            var col = colors[target];
            colors.Remove(target);
            col.Id = to;
            colors.Add(to, col);
        }

        public void SetId(ColorType target, ColorType to) => SetId((int)target, (int)to);

        public void Remove(int id) => colors.Remove(id);

        public void Remove(ColorType type) => Remove((short)type);

        public int AddColor(Color color)
        {
            colors[color.Id] = color;
            return color.Id;
        }

        public List<Color> ToList()
        {
            var list = new List<Color>(colors.Count);
            foreach (var element in colors)
                list.Add(element.Value);
            return list;
        }
    }
}
