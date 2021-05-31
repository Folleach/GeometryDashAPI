using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    [GameBlock(1700)]
    public class CircleParticle : ColorBlock
    {
        [GameProperty("24", (short)Layer.B1)] protected override short zLayer { get; set; } = (short)Layer.B1;
        [GameProperty("25", (short)0)] public override short ZOrder { get; set; } = 0;

        public CircleParticle() : base(1700)
        {
        }
    }
}
