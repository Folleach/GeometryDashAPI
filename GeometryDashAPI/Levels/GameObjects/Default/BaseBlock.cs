using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class BaseBlock : Block
    {
        public virtual short Default_ColorBase { get; protected set; } = (short)ColorType.Obj;

        [GameProperty("21", ColorType.Obj)]
        public short ColorBase { get; set; } = (short)ColorType.Obj;

        public BaseBlock(int id) : base(id)
        {
        }

        public BaseBlock(string[] data)
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
                default:
                    base.LoadProperty(key, value);
                    return;
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
