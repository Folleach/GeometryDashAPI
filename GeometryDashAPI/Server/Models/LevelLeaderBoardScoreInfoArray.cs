using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class LevelLeaderBoardScoreInfoArray : List<LevelLeaderBoardScoreInfo>
    {
        public LevelLeaderBoardScoreInfoArray(string data, int levelID)
        {
            foreach (string infoData in data.Split('|'))
                Add(new LevelLeaderBoardScoreInfo(infoData, levelID));
        }
    }
}
