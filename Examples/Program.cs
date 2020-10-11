using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Memory;
using GeometryDashAPI.Parsers;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

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
            Console.ReadKey();
        }

        private static void F()
        {
            var local = new LocalLevels();
            var w = local.GetLevel("we");
            var level = new Level(w);
            var rand = new Random();
            for (var i = 0; i < 20; i++)
            {
                var block = new BaseBlock(1);
                block.PositionX = rand.Next(0, 100000);
                block.PositionY = rand.Next(0, 2400);
                block.Rotation = 150;
                block.Scale = 2f;
                block.Glow = false;
                level.AddBlock(block);
            }
            
            var sss = level.ToString();
            var aaa = new Level(sss);

            //local.Remove(local.GetLevel("test", 0));
        }
    }
}
