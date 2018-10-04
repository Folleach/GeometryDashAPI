namespace GeometryDashAPI.Levels.GameObjects
{
    public class DetailBlock : Block
    {
        const short Default_ColorDetail = 4;

        public short DetailColor { get; set; } = 4;

        public DetailBlock(int id) : base(id)
        {
        }

        public DetailBlock(string[] data) : base(data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "21":
                        DetailColor = short.Parse(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            if (DetailColor != Default_ColorDetail)
                return $"{base.ToString()},21,{DetailColor}";
            return base.ToString();
        }
    }
}
