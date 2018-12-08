using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Memory;
using System;
using System.Collections.Generic;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            GameProcess game = new GameProcess();
            game.Initialize(Access.PROCESS_VM_READ);
            while (true)
            {
                Console.WriteLine(game.ReadString(0x10E34F8C, 4));
            }
        }
    }
}
