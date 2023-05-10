using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(901)]
    public class MoveTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupID { get; set; }

        [GameProperty("28", 0f, true, Order = OrderTriggerBase + 2)] public float MoveX { get; set; } = 0f;
        [GameProperty("29", 0f, true, Order = OrderTriggerBase + 3)] public float MoveY { get; set; } = 0f; 
        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 4)] public float MoveTime { get; set; } = 0.5f;

        [GameProperty("30", Easing.None, true, Order = OrderTriggerBase + 5)] public Easing EasingMode { get; set; } = Easing.None;
        [GameProperty("85", 2f, true, Order = OrderTriggerBase + 6)] public float EasingTime { get; set; } = 2f;

        [GameProperty("58", false, Order = OrderTriggerBase + 7)] public bool LockPlayerX { get; set; }
        [GameProperty("59", false, Order = OrderTriggerBase + 7)] public bool LockPlayerY { get; set; }

        [GameProperty("71", 0, Order = OrderTriggerBase + 7)] public int TargetPosGroupID { get; set; }
        [GameProperty("100", false, Order = OrderTriggerBase + 8)] public bool UseTarget { get; set; }
        [GameProperty("101", TargetPosGroupType.All, Order = OrderTriggerBase + 8)] public TargetPosGroupType TargetPosGroup { get; set; } = TargetPosGroupType.All;   

        public MoveTrigger() : base(901)
        {
            IsTrigger = true;
        }
    }
}
