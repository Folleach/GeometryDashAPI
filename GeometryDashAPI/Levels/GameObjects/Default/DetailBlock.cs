namespace GeometryDashAPI.Levels.GameObjects.Default
{
    [GameBlock(1658, 1888)]
    public class DetailBlock : Block
    {
        public virtual short Default_ColorDetail { get; protected set; } = 1;

        [GameProperty("22", 1)]
        public short ColorDetail { get; set; } = 1;

        public DetailBlock(int id) : base(id)
        {
        }

        public DetailBlock(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public override void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 21:
                    ColorDetail = short.Parse(value);
                    return;
                default:
                    base.LoadProperty(key, value);
                    return;
            }
        }

        public override string ToString()
        {
            if (ColorDetail != Default_ColorDetail)
                return $"{base.ToString()},21,{ColorDetail}";
            return base.ToString();
        }
    }
}
