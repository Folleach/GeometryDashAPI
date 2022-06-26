using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    [GameBlock(1658, 1888)]
    public class DetailBlock : Block
    {
        [GameProperty("22", (short)1)]
        public short ColorDetail { get; set; } = 1;

        public DetailBlock()
        {
        }
        
        public DetailBlock(int id) : base(id)
        {
        }
    }
}
