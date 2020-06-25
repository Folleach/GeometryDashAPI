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
            Console.WriteLine("Start");
            F();
            Console.WriteLine("F called");
            Console.ReadKey();
        }

        private static async void F()
        {
            GameServer server = new GameServer();

            String result = "";
            foreach (LevelInfo levelInfo in server.GetLevels(new GetLevelsQuery(SearchType.MostLiked)))
            {
                string name = "";
                string author = "";
                if (levelInfo.songInfo.isSongCustom())
                {
                    name = levelInfo.songInfo.Name;
                    author = levelInfo.songInfo.Author;
                }
                else
                {
                    name = levelInfo.songInfo.OfficialSong.ToString();
                }
                result += $"{levelInfo.Name} by {levelInfo.creatorInfo.UserName}, that uses {name} by {author}\n"; // also probably not the best way too do it
            }
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
