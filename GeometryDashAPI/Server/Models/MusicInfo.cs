using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class MusicInfo
    {
        #region fields
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public float Size { get; private set; }
        public string Link { get; private set; }
        public OfficialSong OfficialSong { get; private set; } = OfficialSong.NotChoosen;

    public Dictionary<string, string> WithoutLoaded { get; private set; } = new Dictionary<string, string>();
        #endregion

        public MusicInfo()
        {

        }

        public MusicInfo(string data)
        {
            Load(data);
        }

        public MusicInfo(OfficialSong officialSong)
        {
            this.OfficialSong = officialSong;
        }

        public void Load(string data)
        {
            string[] splitedData = data.Split('|');

            for (int i = 0; i < splitedData.Length - 1; i += 2)
            {
                splitedData[i] = splitedData[i].Replace("~", "");
                String value = splitedData[i + 1].Replace("~", ""); //i mean, should probably do separate variable for each of those functions in Info classes
                //Console.WriteLine($"{splitedData[i]} {value}");
                switch (splitedData[i])
                {
                    case "1":
                        ID = int.Parse(value);
                        break;
                    case "2":
                        Name = value;
                        break;
                    case "4":
                        Author = value;
                        break;
                    case "5":
                        Size = float.Parse(value, CultureInfo.InvariantCulture);
                        break;
                    case "10":
                        Link = value;
                        break;
                    default:
                        WithoutLoaded.Add(splitedData[i], value);
                        break;
                }
            }
        }

        public bool isSongCustom()
        {
            return OfficialSong == OfficialSong.NotChoosen;
        }
    }
}
