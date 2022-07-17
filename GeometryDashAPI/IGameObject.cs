using System.Collections.Generic;

namespace GeometryDashAPI
{
    public interface IGameObject
    {
        List<string> WithoutLoaded { get; }
    }
}
