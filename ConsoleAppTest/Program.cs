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
            while (true)
            {
                gm.Load();
                Console.WriteLine(gm.DataPlist["bootups"]);
                
            //gm.Save();
                //Console.WriteLine("Wait...");
            Console.ReadKey();
            }

            //Stopwatch sw = new Stopwatch();
            //sw.Restart();
            //GameManager gm = new GameManager();
            //gm.UserName = "MyNewName";
            //gm.Save();
            //sw.Stop();
            //Console.WriteLine("Initialize and Save GameManager (ms):" + sw.ElapsedMilliseconds);

            //sw.Restart();
            //LocalLevels ll = new LocalLevels();
            //sw.Stop();
            //Console.WriteLine("Initialize LocalLevels (ms):" + sw.ElapsedMilliseconds);

            //Console.ReadKey();
        }
    }
}
