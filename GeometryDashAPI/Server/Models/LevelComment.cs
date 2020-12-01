using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelComment
    {
        #region fields
        public int ID { get; private set; }
        public int UserID { get; private set; }
        public string UserName { get; private set; }
        public int AccountID { get; private set; } = -1;
        public string Comment { get; private set; }
        public int Likes { get; private set; }
        public string Date { get; private set; }
        public int Percentage { get; private set; }
        public CommenterType AccountType { get; private set; } = CommenterType.Normal;
        public bool IsAccountRegistered { get; private set; } = false;
        #endregion

        public LevelComment(string data)
        {
            string[] fullInfo = data.Split(':');
            string[] arr = fullInfo[0].Split('~');
            for (int i = 0; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case "2":
                        Comment = Encoding.ASCII.GetString(GameConvert.FromBase64(arr[i + 1]));
                        break;
                    case "3":
                        UserID = int.Parse(arr[i + 1]);
                        break;
                    case "4":
                        Likes = int.Parse(arr[i + 1]);
                        break;
                    case "6":
                        ID = int.Parse(arr[i + 1]);
                        break;
                    case "9":
                        Date = arr[i + 1];
                        break;
                    case "10":
                        Percentage = int.Parse(arr[i + 1]);
                        break;
                    case "11":
                        AccountType = (CommenterType) int.Parse(arr[i + 1]);
                        break;
                }
            }
            string[] userData = fullInfo[1].Split('~');
            for (int i = 0; i < userData.Length; i += 2)
            {
                if (userData[i] == "1")
                {
                    UserName = userData[i + 1];
                } else if (userData[i] == "16")
                {
                    IsAccountRegistered = true;
                    AccountID = int.Parse(userData[i + 1]);
                }
            }
        }
    }
}
