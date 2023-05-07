using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public abstract class Trigger : Block, ITrigger
    {
        internal const int OrderTriggerBase = 103;

        [GameProperty("11", false, Order = 100)] public bool TouchTrigger { get; set; }
        [GameProperty("62", false, Order = 101)] public bool SpawnTrigger { get; set; }
        [GameProperty("87", false, Order = 102)] public bool MultiTrigger { get; set; }

        public Trigger()
        {
        }

        public Trigger(int id) : base(id)
        {
        }
    }
}
