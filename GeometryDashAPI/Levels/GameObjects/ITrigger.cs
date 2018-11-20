namespace GeometryDashAPI.Levels.GameObjects
{
    public interface ITrigger
    {
        bool TouchTrigger { get; set; }
        bool SpawnTrigger { get; set; }
        bool MultiTrigger { get; set; }
    }
}
