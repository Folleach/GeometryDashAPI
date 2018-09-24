using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Level.Models;
using GeometryDashAPI.Memory;

namespace ConsoleAppTest
{
    //test library
    class Program
    {        
        static void Main(string[] args)
        {
            GameProcess process = new GameProcess();
            Console.WriteLine("Wait start GeometryDash");
            while (true)
            {
                if (process.Initialize(Access.PROCESS_VM_READ))
                    break;
                Thread.Sleep(10);
            }
            Console.WriteLine("read...");
            while (true)
            {
                float posX = process.Read<float>(process.Game.MainModule, new int[] { 0x003222D0, 0x164, 0x224, 0x4E8, 0xB4, 0x67C });
                Thread.Sleep(1);
            }
        }
    }
}
