using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfo
    {
        public string Data { get; set; } = "";
        public DataType DataType { get; set; }

        public Dictionary<string, string> WithoutLoaded = new Dictionary<string, string>();

        public LevelInfo()
        {

        }

        public LevelInfo(string data)
        {
            string[] splitData = data.Split(':');
            for (int i = 0; i < splitData.Length; i += 2)
            {
                if (i != splitData.Length - 1)
                    switch (splitData[i])
                    {
                        case "2":
                            DataType = DataType.LevelTitle;
                            Data = splitData[i + 1];
                            break;
                        default:
                            if (splitData[i].Contains("~") || splitData[i] == "1")
                                WithoutLoaded.Add(splitData[i], splitData[i + 1]);
                            else
                            {
                                DataType = DataType.CreatorName;
                                Data = splitData[i + 1];
                                break;
                            }
                            continue;
                    }
                break;
            }
        }
    }
}
