using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    public abstract class TargetingTrigger : Trigger
    {
        [GameProperty("51", 0, true)] public int TargetGroupId { get; set; }
        [GameProperty("56", false)] public bool ActivateGroup { get; set; }
        [GameProperty("77", 0)] public int TargetCount { get; set; }
        [GameProperty("80", 0, true)] public int ItemId { get; set; }

        public TargetingTrigger(int id) : base(id)
        {
        }
    }
}