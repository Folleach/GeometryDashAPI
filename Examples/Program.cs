using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects;
using System;
using System.Collections.Generic;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            LocalLevels levels = new LocalLevels();
            List<int> whitelist = new List<int>();
            whitelist.Add(1329);
            var a = levels.GetLevelByName("Temp");
            Level level = new Level(a, null, whitelist);
            foreach (IBlock element in level.Blocks)
                element.PositionY += 30;
            levels.GetLevelByName("Temp").LevelString = level.ToString();
            levels.Save();
            Console.ReadKey();
        }
    }
}
