using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public enum JumpPlateID
    {
        Orange = 35,
        Purple = 140,
        Red = 1332,
        Blue = 67
    }

    public class JumpPlate : Block
    {
        public override Layer Default_ZLayer { get; protected set; } = Layer.B1;
        public override short Default_ZOrder { get; protected set; } = 12;

        public JumpPlateID BlockType
        {
            get => (JumpPlateID)ID;
            set => ID = (int)value;
        }

        public JumpPlate(JumpPlateID type) : base((int)type)
        {
        }

        public JumpPlate(string[] data) : base(data)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
