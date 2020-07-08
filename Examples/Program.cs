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

            string result = "";
            foreach (LevelInfo levelInfo in server.GetLevels(new GetLevelsQuery(SearchType.MostLiked)))
            {
                string name = "";
                string author = "";
                if (levelInfo.MusicInfo.IsSongCustom())
                {
                    name = levelInfo.MusicInfo.Name;
                    author = levelInfo.MusicInfo.Author;
                }
                else
                {
                    name = levelInfo.MusicInfo.OfficialSong.ToString();
                }
                if(levelInfo.CreatorInfo != null)
                    result += $"{levelInfo.Name} by {levelInfo.CreatorInfo.Name}, which uses {name}" + (author == "" ? "" : $" by {author}") + "\n";
                else
                    result += $"{levelInfo.Name} by someone, which uses {name}" + (author == "" ? "" : $" by {author}") + "\n";
            }
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
