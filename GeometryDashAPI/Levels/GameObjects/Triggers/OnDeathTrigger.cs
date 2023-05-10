using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1812)]
    public class OnDeathTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }
        [GameProperty("56", false, false, Order = OrderTriggerBase + 1)] public bool ActivateGroup { get; set; }

        public OnDeathTrigger() : base(1812)
        {
            IsTrigger = true;
        }
    }
}
