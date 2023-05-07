using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1811)]
    public class InstantCountTrigger : TargetingTrigger
    {
        [GameProperty("88", ConditionType.Equals, Order = OrderTriggerBase + 4)] public ConditionType Condition { get; set; } = ConditionType.Equals;

        public InstantCountTrigger() : base(1811)
        {
            IsTrigger = true;
        }

        public override string ToString() => "InstantCountTrigger";
    }
}
