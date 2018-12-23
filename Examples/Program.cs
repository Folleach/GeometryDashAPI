using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Memory;
using System;
using System.Collections.Generic;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            LocalLevels local = new LocalLevels();
            Level level = new Level(local.Levels[0]);
            level.Blocks.RemoveAll(x => true);

            level.AddBlock(new MoveTrigger()
            {
                TargetGroupID = 543,
                MoveX = 10,
                MoveY = -10,
                Time = 0.7f,
                EasingType = Easing.SineIn,
                EasingTime = 1.2f
            });

            local.Levels[0].LevelString = level.ToString();
            local.Save();
        }
    }
}
