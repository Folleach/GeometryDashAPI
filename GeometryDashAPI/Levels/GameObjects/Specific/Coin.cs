using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public class Coin : Block
    {
        public override short Default_ZOrder { get; protected set; } = 9;

        public Coin() : base(1329)
        {
        }

        public Coin(string[] data) : base(data)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
