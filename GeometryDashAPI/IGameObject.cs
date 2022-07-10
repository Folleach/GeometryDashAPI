using System.Collections.Generic;

namespace GeometryDashAPI
{
    public interface IGameObject
    {
        Dictionary<string, string> WithoutLoaded { get; }
    }
}
