using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    // For leaderboard (top)
    // TODO: This is repetition of UserInfo?
    public class PlayerInfo
    {
        public string UserName { get; set; }
        public int UserID { get; set; }
        public int Stars { get; set; }

        Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();

        public PlayerInfo()
        {

        }

        public PlayerInfo(string data)
        {
            string[] splitData = data.Split(':');
            for (int i = 0; i < splitData.Length; i += 2)
            {
                switch (splitData[i])
                {
                    case "1":
                        UserName = splitData[i + 1];
                        continue;
                    case "2":
                        UserID = int.Parse(splitData[i + 1]);
                        continue;
                    case "3":
                        Stars = int.Parse(splitData[i + 1]);
                        continue;
                    default:
                        WithoutLoaded.Add(splitData[i], splitData[i + 1]);
                        continue;
                }
            }
        }

        public override string ToString()
        {
            return $"{UserName} - {UserID}";
        }
    }
}
