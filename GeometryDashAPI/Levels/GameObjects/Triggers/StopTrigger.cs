using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1616)]
    public class StopTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }

        public StopTrigger() : base(1616)
        {
        }
    }
}
