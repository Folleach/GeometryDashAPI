namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public abstract class Trigger : Block, ITrigger
    {
        [GameProperty("11", false)] public bool TouchTrigger { get; set; }
        [GameProperty("62", false)] public bool SpawnTrigger { get; set; }
        [GameProperty("87", false)] public bool MultiTrigger { get; set; }

        public Trigger()
        {
        }

        public Trigger(int id) : base(id)
        {
        }
    }
}
