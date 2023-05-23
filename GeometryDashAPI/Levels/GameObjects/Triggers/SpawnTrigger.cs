using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1268)]
    public class SpawnTrigger : Trigger
    {
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetGroupId { get; set; }
        [GameProperty("102", false, false, Order = OrderTriggerBase + 3)] public float EditorDisable { get; set; }

        public SpawnTrigger() : base(1268)
        {
        }
    }
}
