using System;
using System.Collections.Generic;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Dtos;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Queries;
using GeometryDashAPI.Server.Responses;

namespace Examples
{
    public class ServerExample
    {
        private const string USERNAME = "Folleach";
        private const string PASSWORD = "Your password here";

        public async void Start()
        {
            var server = new GameClient();
            var accountResponse = server.LoginAsync(USERNAME, PASSWORD).Result;
            
            if (accountResponse.GeometryDashStatusCode != 0)
            {
                Console.WriteLine($"Can't login as {USERNAME}. Error code: {accountResponse.GeometryDashStatusCode}");
                return;
            }
            Console.WriteLine($"Success login as {USERNAME}\n");

            var account = accountResponse.GetResultOrDefault();

            var accountInfo = await server.GetAccountAsync(account.AccountId);
            ShowAccount(accountInfo.GetResultOrDefault());
            
            var comments = await server.GetAccountCommentsAsync(account.AccountId, 0);
            ShowComment(comments.GetResultOrDefault()?.Comments);
            
            var myLevels = await server.GetMyLevelsAsync(new PasswordQuery(account.AccountId, PASSWORD), account.UserId, 0);
            ShowLevels(myLevels.GetResultOrDefault());
            
            var top = await server.GetTopAsync(TopType.Top, 100);
            ShowTop(top.GetResultOrDefault());
        }

        private void ShowTop(TopResponse top)
        {
            Console.WriteLine("Top 100:");
            for (var i = 0; i < top.Users.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {top.Users[i].Name}\t{top.Users[i].Stars}★");
            }
        }

        private void ShowLevels(LevelPageResponse myLevels)
        {
            Console.WriteLine("Levels:");
            foreach (var comment in myLevels.Levels)
            {
                Console.WriteLine(new string('-', 24));
                Console.WriteLine(comment.Name);
                Console.WriteLine(comment.Description);
                Console.WriteLine($"{comment.Likes}👍\t{comment.Downloads}⬇");
            }

            Console.WriteLine();
        }

        private void ShowComment(List<AccountComment> commentsComments)
        {
            Console.WriteLine("Comments:");
            foreach (var comment in commentsComments)
            {
                Console.WriteLine(new string('-', 24));
                Console.WriteLine(comment.Comment);
                Console.WriteLine($"{comment.Likes}👍\t{comment.Date} ago");
            }
            
            Console.WriteLine();
        }

        private void ShowAccount(AccountInfoResponse accountInfo)
        {
            var account = accountInfo.Account;
            Console.WriteLine(accountInfo.Account.Name);
            Console.WriteLine($"{account.Stars}★\t{account.Demons}👾\t{account.CreatorPoints}🛠");
            Console.WriteLine(new string('=', 24));
            Console.WriteLine();
        }
    }
}