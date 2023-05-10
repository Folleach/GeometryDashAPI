using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1611)]
    public class CountTrigger : TargetingTrigger
    {
        [GameProperty("104", false, Order = OrderTargetingTriggerBase + 1)] public bool MultiActivate { get; set; }

        public CountTrigger() : base(1611)
        {
            IsTrigger = true;
        }
    }
}
