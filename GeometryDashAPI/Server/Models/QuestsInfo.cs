using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class QuestsInfo : List<QuestInfo>
    {
        public QuestsInfo(string data)
        {
            data = Crypt.XOR(Encoding.ASCII.GetString(GameConvert.FromBase64(data.Split('|')[0].Substring(5))), "19847");
            for (int i = 6; i < 9; i++)
            {
                Add(new QuestInfo(data.Split(':')[i]));
            }
        }
    }
}
