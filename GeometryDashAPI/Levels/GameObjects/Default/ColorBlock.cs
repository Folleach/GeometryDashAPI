using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class ColorBlock : Block
    {
        [GameProperty("21", (short)ColorType.Obj)]
        public virtual short ColorBase { get; set; } = (short)ColorType.Obj;
        [GameProperty("22", (short)1)]
        public virtual short ColorDetail { get; set; } = 1;

        public ColorBlock()
        {
        }

        public ColorBlock(int id) : base(id)
        {
        }
    }
}
