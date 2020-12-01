using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Specific;
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
            string userName = "username";

            var server = new GameServer();
            int myID = server.Login(userName, "password").AccountID;

            var level = server.GetLevels(new GetLevelsQuery(SearchType.Trending))[0];
            Console.WriteLine($"{level.Name} by { level.CreatorInfo.Name}");

            int commentID = server.PostLevelComment(myID, userName, level.ID, $"Nice job, {level.CreatorInfo.Name}", 0);
            string response = server.DeleteLevelComment(myID, level.ID, commentID);

            Console.WriteLine(response == "1" ? "success" : "fail");

            var comments = server.GetLevelComments(level.ID, 0, CommentsSortMode.MostLiked, 200).Where((LevelComment com) => com.Comment.ToLower().Contains("like if")).ToList(); //idk why lol
            if (comments.Count > 0)
                Console.WriteLine(comments[0].Comment);
        }
    }
}
