using GeometryDashAPI;

namespace TestObjects
{
    public class SampleContainer : GameObject
    {
        [GameProperty("1")] public ObjectSample Sample1 { get; set; }
        [GameProperty("2")] public ObjectSample Sample2 { get; set; }

        public override string GetParserSense() => "~";
    }
}