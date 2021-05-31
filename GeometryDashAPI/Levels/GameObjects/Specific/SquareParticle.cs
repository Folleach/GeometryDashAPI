using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    [GameBlock(1586)]
    public class SquareParticle : ColorBlock
    {
        [GameProperty("24", (short)Layer.B2)] protected override short zLayer { get; set; } = (short)Layer.B2;
        [GameProperty("25", (short)0)] public override short ZOrder { get; set; } = 0;

        public SquareParticle() : base(1586)
        {
        }
    }
}
