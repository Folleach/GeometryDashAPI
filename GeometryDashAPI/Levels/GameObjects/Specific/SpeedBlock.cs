using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public enum SpeedBlockId
    {
        Orange = 200,
        Default = 201,
        Green = 202,
        Purple = 203,
        Red = 1334
    }

    [GameBlock(200, 201, 202, 203, 1334)]
    public class SpeedBlock : Block
    {
        [GameProperty("24", (short)Layer.B2)] protected override short zLayer { get; set; } = (short)Layer.B2;
        [GameProperty("25", (short)-6)] public override short ZOrder { get; set; } = -6;

        [GameProperty("13", true)]
        public bool Using { get; set; } = true;

        public SpeedBlockId BlockType
        {
            get => (SpeedBlockId)Id;
            set => Id = (int)value;
        }

        public SpeedBlock() : base(201)
        {
        }

        public SpeedBlock(SpeedBlockId type) : base((int)type)
        {
        }
    }
}
