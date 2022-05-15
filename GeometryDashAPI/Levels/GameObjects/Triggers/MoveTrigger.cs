using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(901)]
    public class MoveTrigger : Trigger
    {
        [GameProperty("51", 0, true)] public int TargetGroupId { get; set; }
        [GameProperty("28", 0, true)] public float MoveX { get; set; }
        [GameProperty("29", 0, true)] public float MoveY { get; set; }
        [GameProperty("10", 0.5f, true)] public float Time { get; set; } = 0.5f;

        public Easing EasingType
        {
            get => (Easing) easingType;
            set => easingType = (byte) value;
        }
        [GameProperty("30", (byte)Easing.None, true)] private byte easingType = (byte)Easing.None;
        [GameProperty("85", 2f, true)] public float EasingTime { get; set; } = 2f;

        public MoveTrigger() : base(901)
        {
        }
    }
}
