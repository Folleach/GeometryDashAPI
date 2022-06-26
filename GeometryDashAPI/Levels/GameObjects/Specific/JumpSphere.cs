using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public enum JumpSphereId
    {
        Yellow = 36,
        Purple = 141,
        Red = 1333,
        LightBlue = 84,
        Green = 1022,
        Dark = 1330,
        ArrowGreen = 1704,
        ArrowPurple = 1751
    }

    [GameBlock(36, 141, 1333, 84, 1022, 1330, 1704, 1751)]
    public class JumpSphere : Block
    {
        [GameProperty("24", (short)Layer.B1)] protected override short zLayer { get; set; } = (short)Layer.B1;
        [GameProperty("25", 12)] public override int ZOrder { get; set; } = 12;

        [GameProperty("99", false)]
        public bool MultiActivate { get; set; }

        public JumpSphereId BlockType
        {
            get => (JumpSphereId)Id;
            set => Id = (int)value;
        }

        public JumpSphere() : base(36)
        {
        }
        
        public JumpSphere(JumpSphereId type) : base((int)type)
        {
        }
    }
}
