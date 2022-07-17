using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameObject : IGameObject
    {
        public List<string> WithoutLoaded { get; set; } = new();
    }
}
