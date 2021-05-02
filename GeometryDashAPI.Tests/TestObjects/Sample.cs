using GeometryDashAPI;

namespace GeometryDashAPI.Tests.TestObjects
{
    public class Sample : GameObject
    {
        [GameProperty("33")] public double X { get; set; }
        
        internal override string ParserSense { get; } = ":";
    }
}