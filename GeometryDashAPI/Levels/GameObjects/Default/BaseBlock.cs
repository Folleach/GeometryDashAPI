using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class BaseBlock : Block
    {
        const short Default_ColorBase = (short)ColorType.Obj;

        public short ColorBase { get; set; } = (short)ColorType.Obj;

        public BaseBlock(int id) : base(id)
        {
        }

        public BaseBlock(string[] data) : base(data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "21":
                        ColorBase = short.Parse(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            if (ColorBase != Default_ColorBase)
                return $"{base.ToString()},21,{ColorBase}";
            return base.ToString();
        }
    }
}
