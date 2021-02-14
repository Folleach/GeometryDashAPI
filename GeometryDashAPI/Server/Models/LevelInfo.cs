using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();

        public LevelInfo()
        {

        }

        public LevelInfo(string data)
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
                    case "3":
                        Description = Encoding.ASCII.GetString(GameConvert.FromBase64(splittedData[i + 1]));
                        continue;
                    default:
                        WithoutLoaded.Add(splittedData[i], splittedData[i + 1]);
                        continue;
                }
            }
        }
    }
}
