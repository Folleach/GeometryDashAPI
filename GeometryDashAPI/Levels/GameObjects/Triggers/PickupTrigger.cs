using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1817)]
    public class PickupTrigger : Trigger
    {
        [GameProperty("80", 0, true, Order = OrderTriggerBase + 1)] public int ItemId { get; set; }
        [GameProperty("77", 0, false, Order = OrderTriggerBase + 2)] public int Count { get; set; }

        public PickupTrigger() : base(1817)
        {
        }
    }
}
