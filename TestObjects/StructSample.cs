using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects
{
    public class StructSample : GameStruct
    {
        [StructPosition(0)] public ObjectSample FirstObject { get; set; }
        [StructPosition(1)] public LargeObject SecondObject { get; set; }
        
        public override string GetParserSense() => "~";
    }
}