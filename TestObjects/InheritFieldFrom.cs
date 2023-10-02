using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects;

[Sense(",")]
public class InheritFieldFrom : GameObject
{
    [GameProperty("1")]
    private int x;

    public int X => x;
}

[Sense(",")]
public class InheritField : InheritFieldFrom
{
    [GameProperty("2")]
    private int y;

    public int Y => y;
}
