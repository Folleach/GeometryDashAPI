using System.Collections.Generic;
using GeometryDashAPI.Levels.GameObjects;

namespace GeometryDashAPI.Levels.Comparers;

public class BlockPositionXComparer : IComparer<IBlock>
{
    public static readonly BlockPositionXComparer Instance = new();

    public int Compare(IBlock x, IBlock y)
    {
        if (ReferenceEquals(x, y))
            return 0;
        if (ReferenceEquals(null, y))
            return 1;
        if (ReferenceEquals(null, x))
            return -1;
        return x.PositionX.CompareTo(y.PositionY);
    }
}
