using System;
using System.Linq;
using GeometryDashAPI.Levels.GameObjects.Specific;

namespace GeometryDashAPI.Levels
{
    public static class LevelDuration
    {
        public static TimeSpan Measure(Level level)
        {
            var maxX = level.Blocks.Max(x => x.PositionX);
            var portals = GetSpeedBlocks(level);

            var seconds = 0d;
            var i = 1;
            for (; i < portals.Length; i++)
                seconds += (portals[i].PositionX - portals[i - 1].PositionX) / portals[i - 1].SpeedType.GetSpeed();

            var final = Math.Min(i, portals.Length - 1);
            var total = seconds + (maxX - portals[final].PositionX) / portals[final].SpeedType.GetSpeed();

            return TimeSpan.FromSeconds(total);
        }

        private static SpeedBlock[] GetSpeedBlocks(Level level)
        {
            return new[] { new SpeedBlock(level.Options.PlayerSpeed) }
                .Concat(level.Blocks.OfType<SpeedBlock>()
                    .Where(x => x.Checked)
                    .OrderBy(x => x.PositionX)
                ).ToArray();
        }
    }
}
