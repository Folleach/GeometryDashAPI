using GeometryDashAPI;

namespace TestObjects
{
    public class ObjectSample : GameObject
    {
        [GameProperty("33")] public double X { get; set; }
        
        public override string GetParserSense() => ":";
    }
}