using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects
{
    [AsStruct]
    [Sense("~")]
    public class StructSample : GameObject
    {
        [GameProperty("0")] public ObjectSample FirstObject { get; set; }
        [GameProperty("1")] public LargeObject SecondObject { get; set; }
        
        public override string GetParserSense() => "~";
    }
}
