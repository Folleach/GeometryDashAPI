using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
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
            for (int i = 0; i < levels.Levels.Count; i++)
            {
                LevelCreatorModel level = levels.Levels[i];
                Console.WriteLine($"{level.Name} - {level.Revision} - {level.Version}");
            }
            Console.ReadKey();
        }
    }
}
