using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class MusicInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MusicInfo()
        {
        }

        public MusicInfo(string data)
        {
            string[] splittedData = data.Split(new string[] { "~|~" }, StringSplitOptions.None);
            for (int i = 0; i < splittedData.Length; i += 2)
            {
                switch (splittedData[i])
                {
                    case "1":
                        ID = int.Parse(splittedData[i + 1]);
                        continue;
                    case "2":
                        Name = splittedData[i + 1];
                        continue;
                }
            }
        }
    }
}
