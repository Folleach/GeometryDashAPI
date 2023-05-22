using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1049)]
    public class ToggleTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }
        [GameProperty("56", false, false, Order = OrderTriggerBase + 2)] public bool ActivateGroup { get; set; }

        public ToggleTrigger() : base(1049) 
        {
        }
    }
}
