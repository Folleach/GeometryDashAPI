using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfoArray : List<LevelInfo>
    {
        public void Load(string data)
        {
            string[] splitedData = data.Split('#');

            string[] splitedLevelsData = splitedData[0].Split('|');
            string[] splitedCreatorsData = splitedData[1].Split('|');
            string[] splitedSongsData = splitedData[2].Split(':');

            List<SongInfo> songsInfo = new List<SongInfo>();
            List<CreatorInfo> creatorsInfo = new List<CreatorInfo>();

            foreach (string songData in splitedSongsData)
                songsInfo.Add(new SongInfo(songData));

            foreach (string creatorData in splitedCreatorsData)
                creatorsInfo.Add(new CreatorInfo(creatorData));

            for (int i = 0; i < splitedLevelsData.Length; i++)
            {
                LevelInfo levelInfo = new LevelInfo(splitedLevelsData[i]);

                if (levelInfo.isSongCustom())
                {
                    foreach (SongInfo songInfo in songsInfo)
                        if (songInfo.ID == levelInfo.SongID)
                            levelInfo.songInfo = songInfo;
                }
                else
                    levelInfo.songInfo = new SongInfo(levelInfo.OfficialSong);

                foreach (CreatorInfo creatorInfo in creatorsInfo)
                    if (creatorInfo.UserID == levelInfo.CreatorID)
                        levelInfo.creatorInfo = creatorInfo;

                Add(levelInfo);
            }
        }
    }
}
