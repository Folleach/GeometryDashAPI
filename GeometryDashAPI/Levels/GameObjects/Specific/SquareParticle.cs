using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public class SquareParticle : ColorBlock
    {
        public override Layer Default_ZLayer { get; protected set; } = Layer.B2;
        public override short Default_ZOrder { get; protected set; } = 0;

        public SquareParticle() : base(1586)
        {
        }

        public SquareParticle(string[] data) : base(data)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
