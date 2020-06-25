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
        public int CreatorID { get; private set; }
        public int DowloadsCount { get; private set; }
        public OfficialSong OfficialSong { get; private set; }
        public int LikesCount { get; private set; }
        public int SongID { get; private set; }
        public Dictionary<string, string> WithoutLoaded { get; private set; } = new Dictionary<string, string>();

        public CreatorInfo creatorInfo { get; set; } = new CreatorInfo();
        public SongInfo songInfo { get; set; } = new SongInfo();
        #endregion

        public LevelInfo()
        {

        }

        public LevelInfo(string data)
        {
            Load(data);
        }

        public LevelInfo(string data, CreatorInfo creatorInfo, SongInfo songInfo)
        {
            this.creatorInfo = creatorInfo;
            this.songInfo = songInfo;
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
                        CreatorID = int.Parse(splitedData[i + 1]);
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
                        SongID = int.Parse(splitedData[i + 1]);
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
