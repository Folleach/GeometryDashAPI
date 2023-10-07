using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects;

[Sense(",")]
public class InheritFieldFrom : GameObject
{
    [GameProperty("1")]
    private int x;

    [GameProperty("3")]
    protected int Id;

    public int X => x;
}

[Sense(",")]
public class InheritField : InheritFieldFrom
{
    [GameProperty("2")]
    private int y;

    public int Y => y;
}
