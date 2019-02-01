using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class PlayerInfoArray : List<PlayerInfo>
    {
        public PlayerInfoArray()
        {
        }

        public void Load(string data)
        {
            string[] splitData = data.Split('|');
            for (int i = 0; i < splitData.Length - 1; i++)
            {
                Add(new PlayerInfo(splitData[i]));
            }
        }
    }
}
