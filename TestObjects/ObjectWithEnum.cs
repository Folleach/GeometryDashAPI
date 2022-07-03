using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects;

[Sense(":")]
public class ObjectWithEnum : GameObject
{
    [GameProperty("1")]
    public SimpleEnum Value { get; set; }
    public override string GetParserSense() => ":";
}
