using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1585)]
    public class AnimateTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }
        [GameProperty("76", 0, true, Order = OrderTriggerBase + 2)] public int AnimateGroupID { get; set; }
        public AnimateTrigger() : base(1585)
        {
            IsTrigger = true;
        }
    }
}
