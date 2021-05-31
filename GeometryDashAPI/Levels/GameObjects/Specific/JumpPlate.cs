using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public enum JumpPlateId
    {
        Yellow = 35,
        Purple = 140,
        Red = 1332,
        LightBlue = 67
    }

    [GameBlock(35, 140, 1332, 67)]
    public class JumpPlate : Block
    {
        [GameProperty("24", (short)Layer.B1)] protected override short zLayer { get; set; } = (short)Layer.B1;
        [GameProperty("25", (short)12)] public override short ZOrder { get; set; } = 12;

        public JumpPlateId BlockType
        {
            get => (JumpPlateId)Id;
            set => Id = (int)value;
        }

        public JumpPlate() : base(35)
        {
        }
        
        public JumpPlate(JumpPlateId type) : base((int)type)
        {
        }
    }
}
