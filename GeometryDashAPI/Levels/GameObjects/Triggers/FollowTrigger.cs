using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1347)]
    public class FollowTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }
        [GameProperty("71", 0, true, Order = OrderTriggerBase + 2)] public int FollowGroupID { get; set; }
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 3)] public float MoveTime { get; set; } = 0.5f;
        [GameProperty("72", 1f, true, Order = OrderTriggerBase + 4)] public float XMod { get; set; } = 1f;
        [GameProperty("73", 1f, true, Order = OrderTriggerBase + 5)] public float YMod { get; set; } = 1f;
        [GameProperty("74", 0, true, Order = OrderTriggerBase + 5)] public int K74 { get; set; }

        public FollowTrigger() : base(1347)
        { 
            IsTrigger = true;
        }
    }
}
