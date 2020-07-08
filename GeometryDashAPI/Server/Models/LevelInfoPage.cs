using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Models
{
    public class LevelInfoPage : List<LevelInfo>
    {
        public Pagination Page { get; set; }
        public string Hash { get; set; }
        public bool HashIsValid
        {
            get
            {
                throw new NotImplementedException();
            }
        }
                
        private Dictionary<int, MusicInfo> music = new Dictionary<int, MusicInfo>();
        private Dictionary<int, KeyValuePair<string, int>> authorAssigment = new Dictionary<int, KeyValuePair<string, int>>();

        public void Load(string data)
        {
            string[] buffer = data.Split('#');
            if (buffer.Length != 5)
                throw new ArgumentException("No matches with the template");
            Page = new Pagination(buffer[3]);
            Hash = buffer[4];
            ReadAuthorAssigment(buffer[1]);
            ReadMusicInfo(buffer[2]);

            var server = new GameServer();

            string[] levelsData = buffer[0].Split('|');
            foreach (var level in levelsData)
            {
                LevelInfo levelInfo = new LevelInfo(level);

                MusicInfo musicInfo;
                KeyValuePair<string, int> authorData;

                if (music.TryGetValue(levelInfo.MusicID, out musicInfo))
                    levelInfo.MusicInfo = musicInfo;
                else
                    levelInfo.MusicInfo = new MusicInfo(levelInfo.OfficialSong);

                if (authorAssigment.TryGetValue(levelInfo.CreatorID, out authorData))
                    levelInfo.CreatorInfo = server.GetAccountInfo(authorData.Value);

                Add(levelInfo);
            }
        }

        private void ReadAuthorAssigment(string data)
        {
            string[] sData = data.Split('|', ':');
            for (int i = 0; i < sData.Length; i += 3)
                authorAssigment.Add(int.Parse(sData[i]), new KeyValuePair<string, int>(sData[i + 1], int.Parse(sData[i + 2])));
        }

        private void ReadMusicInfo(string data)
        {
            string[] splittedData = data.Split(new string[] { "~:~" }, StringSplitOptions.None);
            foreach (var item in splittedData)
            {
                MusicInfo music = new MusicInfo(item);
                this.music.Add(music.ID, music);
            }
        }
    }
}
