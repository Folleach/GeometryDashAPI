using System.Collections.Generic;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();

        public LevelInfo()
        {

        }

        public LevelInfo(string data, Dictionary<int, MusicInfo> music, Dictionary<int, KeyValuePair<string, int>> authors)
        {
            string[] splittedData = data.Split(':');
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
                    default:
                        WithoutLoaded.Add(splittedData[i], splittedData[i + 1]);
                        continue;
                }
            }
        }
    }
}
