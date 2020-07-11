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
using System.Linq;
using System.Text;

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

        private static async void F()
        {
            var server = new GameServer();
            bool result = MessagesSender.SendMessage("USERNAME", "PASSWORD", "Test message", "This message was sent through GeometryDashAPI by Folleach", "zKraX"); //leaking my username, why not?
            Console.WriteLine(result);
        }
    }
}
