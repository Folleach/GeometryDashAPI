using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;
using System;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1815)]
    public class CollisionTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }

        [Obsolete("This value can't be changed in the game, it just exists in this trigger, but absolutely doesn't interact with the game itself")]
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 2)] public float MoveTime { get; set; } = 0.5f;
        [GameProperty("56", false, Order = OrderTriggerBase + 3)] public bool ActivateGroup { get; set; }
        [GameProperty("80", 0, Order = OrderTriggerBase + 4)] public int ABlockId { get; set; }
        [GameProperty("95", 0, Order = OrderTriggerBase + 5)] public int BBlockId { get; set; }
        [GameProperty("93", false, Order = OrderTriggerBase + 6)] public bool TriggerOnExit { get; set; }

        public CollisionTrigger() : base(1815)
        {
        }
    }
}
