using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameObject : IGameObject
    {
        public abstract string GetParserSense();
        public Dictionary<string, string> WithoutLoaded { get; set; } = new();
    }
}
