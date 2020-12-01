using GeometryDashAPI.Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class QuestInfo
    {
        #region fields
        public int ID { get; private set; }
        public QuestType Type { get; private set; }
        public int Amount { get; private set; }
        public int Reward { get; private set; }
        public string Name { get; private set; }
        #endregion

        public QuestInfo(string data)
        {
            string[] splitted = data.Split(',');

            ID = int.Parse(splitted[0]);
            Type = (QuestType)int.Parse(splitted[1]);
            Amount = int.Parse(splitted[2]);
            Reward = int.Parse(splitted[3]);
            Name = splitted[4];
        }
    }
}
