using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    [GameBlock(1329)]
    public class Coin : Block
    {
        [GameProperty("25", (short)9)] public override short ZOrder { get; set; } = 9;

        public Coin() : base(1329)
        {
        }
    }
}
