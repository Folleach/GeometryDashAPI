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

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllLines("ok.txt", Enumerable.Range(1, 1000).Select(x => $"[GameProperty(\"{x}\")] public int x{x}" + " { get; set; }"));
            Console.WriteLine("Call 'F'");
            F().Wait();
            Console.WriteLine("'F' called");
            Console.ReadKey();
        }

        private static async Task F()
        {
            var level = await new GameServer().DownloadLevel(64144455);
            Console.WriteLine(level.Name);
        }
    }
}
