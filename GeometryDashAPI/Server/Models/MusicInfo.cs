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
            string[] splitedData = data.Split(new string[] { "~|~" }, StringSplitOptions.None);

            for (int i = 0; i < splitedData.Length - 1; i += 2)
            {
                switch (splitedData[i])
                {
                    case "1":
                        ID = int.Parse(splitedData[i + 1]);
                        break;
                    case "2":
                        Name = splitedData[i + 1];
                        break;
                    case "4":
                        Author = splitedData[i + 1];
                        break;
                    case "5":
                        Size = float.Parse(splitedData[i + 1], CultureInfo.InvariantCulture);
                        break;
                    case "10":
                        Link = splitedData[i + 1];
                        break;
                    default:
                        WithoutLoaded.Add(splitedData[i], splitedData[i + 1]);
                        break;
                }
            }
        }

        public bool IsSongCustom()
        {
            return OfficialSong == OfficialSong.NotChoosen;
        }
    }
}
