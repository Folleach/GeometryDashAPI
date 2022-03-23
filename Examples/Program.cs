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
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Specific;
using GeometryDashAPI.Parsers;
using GeometryDashAPI.Server.Dtos;
using GeometryDashAPI.Server.Responses;
using Newtonsoft.Json;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static async Task Main(string[] args)
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
            var level = new Level(File.ReadAllText(@"C:\Users\Andrey\Desktop\New Text Document (7).txt"), true);
            foreach (var block in level.Blocks.OfType<TextBlock>())
            {
                Console.WriteLine(block.Text);
            }
            Console.WriteLine("end");
            // var loginResponse = new GameServer(network: new Network(Encoding.ASCII, () => new HttpClientHandler()
            // {
            //     Proxy = new WebProxy("socks5://180.76.146.170:1080"),
            //     UseProxy = true
            // },
            //     responseFilter: s =>
            //     {
            //         Console.WriteLine(s);
            //         return true;
            //     })).GetAccountInfo(71).Result;
            // Console.WriteLine($"HttpStatusCode: {loginResponse.HttpStatusCode}");
            // Console.WriteLine($"GdStatusCode: {loginResponse.GeometryDashStatusCode}");
            // Console.WriteLine($"Result: " + (loginResponse.GetResultOrDefault() == null
            //     ? "null"
            //     : loginResponse.GetResultOrDefault()?.Account.Name));
        }
    }
}
