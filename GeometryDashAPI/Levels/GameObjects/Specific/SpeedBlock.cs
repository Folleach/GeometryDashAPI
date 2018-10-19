using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public enum SpeedBlockID
    {
        Orange = 200,
        Default = 201,
        Green = 202,
        Purple = 203,
        Red = 1334
    }

    public class SpeedBlock : Block
    {
        public override Layer Default_ZLayer { get; protected set; } = Layer.B2;
        public override short Default_ZOrder { get; protected set; } = -6;

        public bool Used { get; set; } = true;

        public SpeedBlockID BlockType
        {
            get => (SpeedBlockID)ID;
            set => ID = (int)value;
        }

        public SpeedBlock(SpeedBlockID type) : base((int)type)
        {
        }

        public SpeedBlock(string[] data) : base(data)
        {
            
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "13":
                        Used = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()},13,{GameConvert.BoolToString(Used)}";
        }
    }
}
