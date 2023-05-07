using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public abstract class TargetingTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }
        [GameProperty("56", false)] public bool ActivateGroup { get; set; }
        [GameProperty("77", 0, Order = OrderTriggerBase + 3)] public int TargetCount { get; set; }
        [GameProperty("80", 0, true, Order = OrderTriggerBase + 2)] public int ItemId { get; set; }

        public TargetingTrigger(int id) : base(id)
        {
        }
    }
}