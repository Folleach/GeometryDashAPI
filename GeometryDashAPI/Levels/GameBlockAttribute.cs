using System;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels
{
    public class GameBlockAttribute : Attribute
    {
        public readonly int[] Ids;

        public GameBlockAttribute(params int[] ids)
        {
            Ids = ids;
        }
    }
}