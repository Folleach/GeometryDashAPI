using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects;

[Sense(":")]
public class WithNullable : GameObject
{
    [GameProperty("3")]
    public int? Value { get; set; }
}
