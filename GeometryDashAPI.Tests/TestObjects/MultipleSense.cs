namespace GeometryDashAPI.Tests.TestObjects
{
    public class MultipleSense : GameObject
    {
        [GameProperty("1")] public int X1 { get; set; }
        
        public override string GetParserSense() => "~|~";
    }
}