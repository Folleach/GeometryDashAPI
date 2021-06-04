namespace GeometryDashAPI.Tests.TestObjects
{
    public class SampleContainer : GameObject
    {
        [GameProperty("1")] public Sample Sample1 { get; set; }
        [GameProperty("2")] public Sample Sample2 { get; set; }

        public override string GetParserSense() => "~";
    }
}