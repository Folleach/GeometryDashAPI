using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class Portal : Block
    {
        [GameProperty("13", true, true)] public bool Checked { get; set; }

        public Portal()
        {
        }
        
        public Portal(int id) : base(id)
        {
        }
    }
}
