using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public abstract class TargetingTrigger : Trigger
    {
        internal const int OrderTargetingTriggerBase = 107;

        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }
        [GameProperty("80", 0, true, Order = OrderTriggerBase + 2)] public int ItemId { get; set; }
        [GameProperty("77", 0, Order = OrderTriggerBase + 3)] public int TargetCount { get; set; }
        [GameProperty("56", false, Order = OrderTriggerBase + 4)] public bool ActivateGroup { get; set; }

        public TargetingTrigger(int id) : base(id)
        { }
    }
}