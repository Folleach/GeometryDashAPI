using System;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels
{
    public class GameBlockAttribute : Attribute
    {
        public readonly int[] Ids;

        public GameBlockAttribute(
            // int defaultColorBase,
            // int defaultColorDetail,
            // int defaultZOrder,
            // Layer defaultZLayer,
            params int[] ids)
        {
            // DefaultColorBase = defaultColorBase;
            // DefaultColorDetail = defaultColorDetail;
            // DefaultZOrder = defaultZOrder;
            // DefaultZLayer = defaultZLayer;
            Ids = ids;
        }
    }
}