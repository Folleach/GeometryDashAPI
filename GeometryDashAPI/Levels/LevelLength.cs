using System;
using System.Collections.Generic;
using System.Linq;
using GeometryDashAPI.Levels.GameObjects.Specific;

namespace GeometryDashAPI.Levels
{
    public class LevelLength
    {
        public TimeSpan TimeLength { get; }
        public int Seconds { get; }

        private LevelLength(double seconds)
        {
            TimeLength = TimeSpan.FromSeconds(seconds);
            Seconds = (int)seconds; // basically (int)Math.Floor(seconds), geometry dash floors the doubles
        }
        
        public static LevelLength Measure(Level level)
        {
            var x = level.Blocks.Max(x => x.PositionX);
            var portals = GetSpeedBlocks(level);
            
            double seconds = 0;
            int i = 1;
            for (; i < portals.Count && portals.ElementAt(i).PositionX < x; i++)
                seconds += (portals.ElementAt(i).PositionX - portals.ElementAt(i - 1).PositionX) / portals.ElementAt(i - 1).SpeedType.GetSpeed();
            
            int final = Math.Min(i, portals.Count - 1);
            var total = seconds + (x - portals.ElementAt(final).PositionX) / portals.ElementAt(final).SpeedType.GetSpeed();

            return new LevelLength(total);
        }

        private static SortedSet<SpeedBlock> GetSpeedBlocks(Level level)
        {
            var speedPortals = new SpeedBlock[] { new SpeedBlock(level.Options.PlayerSpeed) }.Concat(level.Blocks.OfType<SpeedBlock>().Where(x => x.Checked).OrderBy(x => x.PositionX));
            return new SortedSet<SpeedBlock>(speedPortals);
        }
    }   
}