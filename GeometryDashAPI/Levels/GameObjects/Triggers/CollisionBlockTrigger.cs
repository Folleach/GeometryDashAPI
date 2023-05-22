using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1816)]
    public class CollisionBlockTrigger : Trigger
    {
        [GameProperty("80", 0, true, Order = OrderTriggerBase + 1)] public int BlockId { get; set; }
        [GameProperty("94", false, false, Order = OrderTriggerBase + 2)] public bool DynamicBlock { get; set; }

        public CollisionBlockTrigger() : base(1816)
        {
        }
    }
}
