using GeometryDashAPI.Levels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public struct Guideline
    {
        public double Timestamp { get; set; }
        public GuidelineColor Color { get; set; }

        public override string ToString()
        {
            return $"TimeStamp: {GameConvert.DoubleToString(Timestamp)}, " +
            $"Color: {Color}";
        }
    }
}
