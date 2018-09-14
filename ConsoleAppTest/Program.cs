using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Enums;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            Console.WriteLine(gm.DataPlist["bootups"]);
            Console.ReadKey();
        }
    }
}
