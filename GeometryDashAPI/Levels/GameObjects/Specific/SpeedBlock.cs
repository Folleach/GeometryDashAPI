using System;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    /// <summary>
    /// Represents the id of the speed block.<br/><br/>
    /// Not to be confused with <see cref="SpeedType"/>.<br/>
    /// Because it is responsible for a specific speed, instead of a specific block id
    /// </summary>
    public enum SpeedBlockId
    {
        Orange = 200,
        Default = 201,
        Green = 202,
        Purple = 203,
        Red = 1334
    }

    [GameBlock(200, 201, 202, 203, 1334)]
    public class SpeedBlock : Portal
    {
        [GameProperty("24", (short)Layer.B2)] protected override short zLayer { get; set; } = (short)Layer.B2;
        [GameProperty("25", -6)] public override int ZOrder { get; set; } = -6;

        public SpeedBlockId BlockType
        {
            get => (SpeedBlockId)Id;
            set => Id = (int)value;
        }

        public SpeedType SpeedType
        {
            get => FromBlockIdToSpeedType(BlockType);
            set => BlockType = FromSpeedTypeToBlockId(value);
        }

        public SpeedBlock() : base(201)
        {
        }

        public SpeedBlock(SpeedBlockId type) : base((int)type)
        {
        }

        public SpeedBlock(SpeedType type) : base((int)FromSpeedTypeToBlockId(type))
        {
        }

        public static SpeedType FromBlockIdToSpeedType(SpeedBlockId id)
        {
            return id switch
            {
                SpeedBlockId.Orange => SpeedType.Half,
                SpeedBlockId.Default => SpeedType.Default,
                SpeedBlockId.Green => SpeedType.X2,
                SpeedBlockId.Purple => SpeedType.X3,
                SpeedBlockId.Red => SpeedType.X4,
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
        }

        public static SpeedBlockId FromSpeedTypeToBlockId(SpeedType speedType)
        {
            return speedType switch
            {
                SpeedType.Half => SpeedBlockId.Orange,
                SpeedType.Default => SpeedBlockId.Default,
                SpeedType.X2 => SpeedBlockId.Green,
                SpeedType.X3 => SpeedBlockId.Purple,
                SpeedType.X4 => SpeedBlockId.Red,
                _ => throw new ArgumentOutOfRangeException(nameof(speedType), speedType, null)
            };
        }
    }
}
