namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class DetailBlock : Block
    {
        const short Default_ColorDetail = 1;

        public short ColorDetail { get; set; } = 1;

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
                        ColorDetail = short.Parse(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
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
