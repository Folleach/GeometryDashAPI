using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Memory;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Call 'F'");
            F();
            Console.WriteLine("'F' called");
            //Console.ReadKey();
        }

        private static void F()
        {
            var levels = new LocalLevels();
            new Level(levels.GetLevel("sti"));
            var downloaded = new GameServer().DownloadLevel(69648515).Result.LevelString;
            var stopwatch = Stopwatch.StartNew();
            var level = new Level(downloaded, true);
            stopwatch.Stop();

            levels.GetLevel("str").LevelString = level.ToString();
            levels.Save();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
