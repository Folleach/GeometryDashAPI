using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects
{
    [Sense(":")]
    public class ObjectSample : GameObject
    {
        [GameProperty("33")] public double X { get; set; }
        
        public override string GetParserSense() => ":";
    }
}
