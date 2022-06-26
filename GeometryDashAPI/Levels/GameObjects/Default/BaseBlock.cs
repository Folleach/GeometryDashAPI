using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    [GameBlock(1, 8)]
    public class BaseBlock : Block
    {
        [GameProperty("21", (short)ColorType.Obj)]
        public virtual short ColorBase { get; set; } = (short)ColorType.Obj;

        public BaseBlock()
        {
        }
        
        public BaseBlock(int id) : base(id)
        {
        }
    }
}
