using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects
{
    public class ColorBlock : Block
    {
        const short Default_ColorBase = (short)ColorType.Obj;
        const short Default_ColorDetail = 1;

        public short ColorBase { get; set; } = 1;
        public short ColorDetail { get; set; } = 1;

        public ColorBlock(int id) : base(id)
        {
        }

        public ColorBlock(string[] data) : base(data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "21":
                        ColorBase = short.Parse(data[i + 1]);
                        continue;
                    case "22":
                        ColorDetail = short.Parse(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            if (ColorBase != Default_ColorDetail)
                return $"{base.ToString()},21,{ColorBase}";
            if (ColorDetail != Default_ColorDetail)
                return $"{base.ToString()},22,{ColorDetail}";
            return base.ToString();
        }
    }
}
