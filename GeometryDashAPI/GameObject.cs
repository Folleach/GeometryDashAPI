using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameObject
    {
        public abstract string GetParserSense();
        public Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();
    }
}