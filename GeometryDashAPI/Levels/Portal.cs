using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels
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