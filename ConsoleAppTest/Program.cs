using System;
using System.Diagnostics;
using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;

namespace ConsoleAppTest
{
    //test library
    class Program
    {
        static void Main(string[] args)
        {
            LocalLevels ls = new LocalLevels();
            GameManager gm = new GameManager();
            Stopwatch sw = new Stopwatch();

            while (true)
            {
                Console.ReadKey();
                Level level = new Level(ls.Levels[0]);
                level.Colors.AddColor(new Color((short)ColorType.Background)
                {
                    Red = 255,
                    Green = 255,
                    Blue = 0
                });
                ls.Levels[0].LevelString = level.ToString();
                ls.Save();
            }
        }
    }
}
