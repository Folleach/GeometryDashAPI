using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class LevelLeaderBoardScoreInfo
    {
        #region fields
        public string UserName { get; private set; }
        public int AccountID { get; private set; }
        public int LevelID { get; private set; }
        public int Position { get; private set; }
        public int Percentage { get; private set; }
        public int Coins { get; private set; }
        public string Timestamp { get; private set; }
        #endregion

        public LevelLeaderBoardScoreInfo(string data, int levelID)
        {
            LevelID = levelID;

            GameServer server = new GameServer();
            string[] splitted = data.Split(':');
            for (int i = 0; i < splitted.Length; i+=2)
            {
                switch (int.Parse(splitted[i]))
                {
                    case 1:
                        UserName = splitted[i + 1];
                        break;
                    case 16:
                        AccountID = int.Parse(splitted[i + 1]);
                        break;
                    case 6:
                        Position = int.Parse(splitted[i + 1]);
                        break;
                    case 3:
                        Percentage = int.Parse(splitted[i + 1]);
                        break;
                    case 13:
                        Coins = int.Parse(splitted[i + 1]);
                        break;
                    case 42:
                        Timestamp = splitted[i + 1];
                        break;
                }
            }
        }
    }
}
