using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1007)]
    public class AlphaTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetID { get; set; } = 0;
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 2)] public float FadeTime { get; set; } = 0.5f;
        [GameProperty("35", 1f, true, Order = OrderTriggerBase + 3)] public float Opacity { get; set; } = 1f;

        public AlphaTrigger() : base(1007)
        {
            IsTrigger = true;
        }

        public override string ToString() => "AlphaTrigger";
    }
}
