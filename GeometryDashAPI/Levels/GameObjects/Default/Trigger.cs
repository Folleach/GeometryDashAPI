using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public abstract class Trigger : Block, ITrigger
    {
        internal const int OrderTriggerBase = 103;

        [GameProperty("11", false, Order = 100)] public bool TouchTrigger { get; set; } = false;
        [GameProperty("62", false, Order = 101)] public bool SpawnTrigger { get; set; } = false;
        [GameProperty("87", false, Order = 102)] public virtual bool MultiTrigger { get; set; } = false;

        public Trigger()
        {
        }

        public Trigger(int id) : base(id)
        {
        }
    }
}
