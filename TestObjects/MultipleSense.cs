using GeometryDashAPI;
using GeometryDashAPI.Attributes;

namespace TestObjects
{
    [Sense("~|~")]
    public class MultipleSense : GameObject
    {
        [GameProperty("1")] public int X1 { get; set; }
    }
}
