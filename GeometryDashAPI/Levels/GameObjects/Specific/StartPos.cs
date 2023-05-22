using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    [GameBlock(31)]
    public class StartPos : Block
    {
        [GameProperty("kA2", GameMode.Cube, true, KeyOverride = 0)] public GameMode GameMode { get; set; } = GameMode.Cube;
        [GameProperty("kA3", false, true, KeyOverride = 1)] public bool IsMini { get; set; }
        [GameProperty("kA8", false, true, KeyOverride = 2)] public bool IsDual { get; set; }
        [GameProperty("kA4", SpeedType.Default, true, KeyOverride = 3)] public SpeedType PlayerSpeed { get; set; } = SpeedType.Default;
        [GameProperty("kA11", false, true, KeyOverride = 4)] public bool IsFlippedGravity { get; set; }
        [GameProperty("kA10", 0, true, KeyOverride = 5)] public int Ka10 { get; set; }

        public StartPos() : base(31)
        { }
    }
}
