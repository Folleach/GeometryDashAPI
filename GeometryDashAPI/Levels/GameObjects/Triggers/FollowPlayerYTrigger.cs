using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1814)]
    public class FollowPlayerYTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 2)] public float MoveTime { get; set; } = 0.5f;
        [GameProperty("90", 1f, true, Order = OrderTriggerBase + 3)] public float Speed { get; set; } = 1f;
        [GameProperty("91", 0, false, Order = OrderTriggerBase + 4)] public float Delay { get; set; }
        [GameProperty("92", 0, false, Order = OrderTriggerBase + 5)] public float Offset { get; set; }
        [GameProperty("105", 0, false, Order = OrderTriggerBase + 6)] public float MaxSpeed { get; set; }

        public FollowPlayerYTrigger() : base(1814)
        {
            IsTrigger = true;
        }
    }
}
