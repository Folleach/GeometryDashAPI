using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameObject
    {
        internal abstract string ParserSense { get; }
        public Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();
    }
}