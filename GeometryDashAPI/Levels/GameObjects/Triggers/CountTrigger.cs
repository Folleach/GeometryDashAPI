namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1611)]
    public class CountTrigger : TargetingTrigger
    {
        [GameProperty("104", false)] public bool MultiActivate { get; set; }

        public CountTrigger() : base(1611)
        {
            IsTrigger = true;
        }
    }
}
