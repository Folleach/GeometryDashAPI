using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameObject
    {
        public abstract string ParserSense { get; }
        public Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();
    }
}