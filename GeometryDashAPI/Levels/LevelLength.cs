using System;
using System.Collections.Generic;
using System.Linq;
using GeometryDashAPI.Levels.Comparers;
using GeometryDashAPI.Levels.GameObjects.Specific;

namespace GeometryDashAPI.Levels
{
    public static class LevelLength
    {
        public static TimeSpan Measure(Level level)
        {
            var x = level.Blocks.Max(x => x.PositionX);
            var portals = GetSpeedBlocks(level);

            double seconds = 0;
            var i = 1;
            for (; i < portals.Count && portals.ElementAt(i).PositionX < x; i++)
                seconds += (portals.ElementAt(i).PositionX - portals.ElementAt(i - 1).PositionX) / portals.ElementAt(i - 1).SpeedType.GetSpeed();

            var final = Math.Min(i, portals.Count - 1);
            var total = seconds + (x - portals.ElementAt(final).PositionX) / portals.ElementAt(final).SpeedType.GetSpeed();

            return TimeSpan.FromSeconds(total);
        }

        private static SortedSet<SpeedBlock> GetSpeedBlocks(Level level)
        {
            var speedPortals = new SpeedBlock[] { new SpeedBlock(level.Options.PlayerSpeed) }.Concat(level.Blocks.OfType<SpeedBlock>().Where(x => x.Checked).OrderBy(x => x.PositionX));
            return new SortedSet<SpeedBlock>(speedPortals, BlockPositionXComparer.Instance);
        }
    }
}
