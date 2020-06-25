using GeometryDashAPI.Levels.Enums;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class ColorBlock : Block
    {
        public virtual short Default_ColorBase { get; protected set; } = (short)ColorType.Obj;
        public virtual short Default_ColorDetail { get; protected set; } = 1;

        public short ColorBase { get; set; } = (short)ColorType.Obj;
        public short ColorDetail { get; set; } = 1;

        public ColorBlock()
        {
        }

        public ColorBlock(int id) : base(id)
        {
        }

        public ColorBlock(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public override void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 21:
                    ColorBase = short.Parse(value);
                    return;
                case 22:
                    ColorDetail = short.Parse(value);
                    return;
                default:
                    base.LoadProperty(key, value);
                    return;
            }
            
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            if (ColorBase != Default_ColorBase)
                builder.Append($",21,{ColorBase}");
            if (ColorDetail != Default_ColorDetail)
                builder.Append($",22,{ColorDetail}");
            return builder.ToString();
        }
    }
}
