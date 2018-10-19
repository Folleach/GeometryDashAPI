using GeometryDashAPI.Levels.GameObjects.Default;
using System;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public class TextBlock : DetailBlock
    {
        public override short Default_ZOrder { get; protected set; } = 1;

        public string Text { get; set; } = "A";

        public TextBlock() : base(914)
        {
        }

        public TextBlock(string[] data) : base(data)
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "31":
                        Text = Encoding.ASCII.GetString(Convert.FromBase64String(data[i + 1]));
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()},31,{Convert.ToBase64String(Encoding.ASCII.GetBytes(Text))}";
        }
    }
}
