using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1346)]
    public class RotateTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }
        [GameProperty("71", 0, true, Order = OrderTriggerBase + 2)] public int CenterGroupID { get; set; }
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 3)] public float MoveTime { get; set; } = 0.5f;
        [GameProperty("30", Easing.None, true, Order = OrderTriggerBase + 4)] public Easing Easing { get; set; } = Easing.None;
        [GameProperty("85", 2f, true, Order = OrderTriggerBase + 5)] public float EasingTime { get; set; } = 2f;
        [GameProperty("68", 0, true, Order = OrderTriggerBase + 6)] public int Degrees { get; set; }
        [GameProperty("69", 0, true, Order = OrderTriggerBase + 7)] public int Times360 { get; set; }
        [GameProperty("70", false, true, Order = OrderTriggerBase + 8)] public bool LockObjectRotation { get; set; } = false;

        public RotateTrigger() : base(1346)
        {
            IsTrigger = true;
        }
    }
}
