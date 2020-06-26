using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfo
    {
        #region fields
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Version { get; private set; }
        public int AuthorID { get; private set; }
        public int DowloadsCount { get; private set; }
        public OfficialSong OfficialSong { get; private set; }
        public int LikesCount { get; private set; }
        public int MusicID { get; private set; }
        public Dictionary<string, string> WithoutLoaded { get; private set; } = new Dictionary<string, string>();
        public string AuthorName { get; set; }

        public MusicInfo MusicInfo { get; set; } = new MusicInfo();
        #endregion

        public LevelInfo()
        {

        }

        public LevelInfo(string data)
        {
            Load(data);
        }

        public LevelInfo(string data, MusicInfo songInfo, string creatorName)
        {
            AuthorName = creatorName;
            MusicInfo = songInfo;
            Load(data);
        }

        public void Load(string data)
        {
            string[] splitedData = data.Split(':');

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
                    case "5":
                        Version = int.Parse(splitedData[i + 1]);
                        break;
                    case "6":
                        AuthorID = int.Parse(splitedData[i + 1]);
                        break;
                    case "10":
                        DowloadsCount = int.Parse(splitedData[i + 1]);
                        break;
                    case "12":
                        int value = int.Parse(splitedData[i + 1]);
                        OfficialSong = (OfficialSong)int.Parse(splitedData[i + 1] + 1);
                        break;
                    case "14":
                        LikesCount = int.Parse(splitedData[i + 1]);
                        break;
                    case "35":
                        MusicID = int.Parse(splitedData[i + 1]);
                        OfficialSong = OfficialSong.NotChoosen; // probably not the best way to do this, especially considering the fact that it's not too accurate enum name
                        break;
                    default:
                        WithoutLoaded.Add(splitedData[i], splitedData[i + 1]);
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
