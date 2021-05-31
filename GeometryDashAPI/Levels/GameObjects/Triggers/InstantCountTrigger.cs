using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1811)]
    public class InstantCountTrigger : TargetingTrigger
    {
        public ConditionType Condition
        {
            get => (ConditionType)condition;
            set => condition = (byte)value;
        }
        [GameProperty("88", (byte)ConditionType.Equals)] private byte condition = (byte)ConditionType.Equals;

        public InstantCountTrigger() : base(1811)
        {
            IsTrigger = true;
        }
    }
}
