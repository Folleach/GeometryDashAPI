using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1585)]
    public class AnimateTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }
        [GameProperty("76", 0, true, Order = OrderTriggerBase + 2)] public int AnimateGroupId { get; set; }

        public AnimateTrigger() : base(1585)
        {
        }
    }
}
