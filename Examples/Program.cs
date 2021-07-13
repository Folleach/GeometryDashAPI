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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Server.Dtos;
using GeometryDashAPI.Server.Responses;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            
            Console.WriteLine("Call 'F'");
            F();
            Console.ReadLine();
            Console.WriteLine("'F' called");
            //Console.ReadKey();
        }

        private static void F()
        {
            var loginResponse = new GameServer().Login("folleach", "***********").Result;
            Console.WriteLine($"HttpStatusCode: {loginResponse.HttpStatusCode}");
            Console.WriteLine($"GdStatusCode: {loginResponse.GeometryDashStatusCode}");
            Console.WriteLine($"Result: " + (loginResponse.GetResultOrDefault() == null
                ? "null"
                : loginResponse.GetResultOrDefault().AccountId.ToString()));
        }
    }
}
