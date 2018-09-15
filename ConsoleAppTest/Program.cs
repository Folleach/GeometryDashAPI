using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Level.Models;

namespace ConsoleAppTest
{
    class Program
    {
        //test library
        static void Main(string[] args)
        {
            LocalLevels gm = new LocalLevels();
            gm.GetLevelByName("test").NormalProgress = 55;
            gm.GetLevelByName("test").PracticeProgress = 54;
            gm.GetLevelByName("test").TotalAttempts = 117;
            gm.GetLevelByName("test").TotalJumps = 999;
            gm.Save();
            Console.WriteLine("a");
        }
    }
}
