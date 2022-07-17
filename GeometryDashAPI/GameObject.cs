using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameObject : IGameObject
    {
        public Dictionary<string, string> WithoutLoaded { get; set; } = new();
    }
}
