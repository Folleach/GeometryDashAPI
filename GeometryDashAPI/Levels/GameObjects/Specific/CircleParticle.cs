using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public class CircleParticle : ColorBlock
    {
        public override Layer Default_ZLayer { get; protected set; } = Layer.B1;
        public override short Default_ZOrder { get; protected set; } = 0;

        public CircleParticle() : base(1700)
        {
        }

        public CircleParticle(string[] data) : base(data)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
