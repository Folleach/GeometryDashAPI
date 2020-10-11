using GeometryDashAPI.Levels.Enums;
using System;

namespace GeometryDashAPI.Levels
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GameObjectAttribute : Attribute
    {
        public Layer DefaultZLayer { get; }
        public short DefaultZOrder { get; }

        public GameObjectAttribute(Layer zLayer, short zOrder)
        {
            DefaultZLayer = zLayer;
            DefaultZOrder = zOrder;
        }
    }
}
