using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels
{
    public static class LevelSpeed
    {
        public const double Half = 251.16;
        public const double Default = 311.58;
        public const double X2 = 387.42;
        public const double X3 = 468;
        public const double X4 = 576;

        public static double GetSpeed(this SpeedType speedType)
        {
            return speedType switch
            {
                SpeedType.Half => Half,
                SpeedType.Default => Default,
                SpeedType.X2 => X2,
                SpeedType.X3 => X3,
                SpeedType.X4 => X4
            };
        }
    }
}
