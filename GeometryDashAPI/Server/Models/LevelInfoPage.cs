using System;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Parser;
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

        public void Load(string data)
        {
            if (data == "-1")
                return;
            string[] buffer = data.Split('#');
            if (buffer.Length != 5)
                throw new ArgumentException("No matches with the template");
            Page = new Pagination(buffer[3]);
            Hash = buffer[4];
            var authorAssigment = GetAuthorAssigment(buffer[1]);
            ReadMusicInfo(buffer[2]);

            string[] levelsData = buffer[0].Split('|');
            foreach (var level in levelsData)
                Add(ObjectParser.Decode<LevelInfo>(level));
        }

        private Dictionary<int, KeyValuePair<string, int>> GetAuthorAssigment(string data)
        {
            string[] sData = data.Split('|', ':');
            var result = new Dictionary<int, KeyValuePair<string, int>>();
            for (int i = 0; i < sData.Length; i += 3)
                result.Add(int.Parse(sData[i]), new KeyValuePair<string, int>(sData[i + 1], int.Parse(sData[i + 2])));
            return result;
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
