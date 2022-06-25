using System.Collections.Generic;

namespace GeometryDashAPI
{
    public abstract class GameStruct : IGameObject
    {
        public abstract string GetParserSense();
        public Dictionary<string, string> WithoutLoaded { get; }
    }
}