using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public abstract class Trigger : Block, ITrigger
    {
        internal const int OrderTriggerBase = 103;

        [GameProperty("11", false, Order = 100)] public bool TouchTrigger { get; set; } = false;
        [GameProperty("62", false, Order = 101)] public bool SpawnTrigger { get; set; } = false;
        [GameProperty("87", false, Order = 102)] protected bool multiTrigger;

        public virtual bool MultiTrigger
        {
            get => multiTrigger;
            set => multiTrigger = value;
        }

        public Trigger()
        {
            IsTrigger = true;
        }

        public Trigger(int id) : base(id)
        {
            IsTrigger = true;
        }
    }
}
