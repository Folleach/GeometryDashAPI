using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Interfaces;
using System;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            LocalLevels levels = new LocalLevels();
            Level level = new Level(levels.GetLevelByName("Temp"));
            foreach (IBlock element in level.Blocks)
                Console.WriteLine(element.ID);
            Console.ReadKey();
        }
    }
}
