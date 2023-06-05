using System;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Enums;

namespace GeometryDashAPI.Server.Dtos
{
    [Sense(":")]
    public class LevelPreview : GameObject
    {
        [GameProperty("1")]
        public int Id { get; set; }

        [GameProperty("2")]
        public string Name { get; set; }

        [GameProperty("3")]
        private string description;
        public string Description
        {
            get
            {
                var value = description;
                if ((value.Length % 4) != 0)
                    value = $"{value}{new string('=', 4 - value.Length % 4)}";
                return GameConvert.FromBase64String(value);
            }
            set
            {
                var base64 = GameConvert.ToBase64String(value);
                description = base64.Length > 255 ? base64.Remove(255, base64.Length - 255) : base64;
            }
        }

        [GameProperty("5")]
        public int Version { get; set; }
        
        [GameProperty("6")]
        public int AuthorUserId { get; set; }
        
        [GameProperty("8")]
        public Difficulty Difficulty { get; set; }
        
        [GameProperty("9")]
        public DifficultyIcon DifficultyIcon { get; set; }
        
        [GameProperty("10")]
        public int Downloads { get; set; }
        
        [Obsolete]
        [GameProperty("11")]
        public int Completes { get; set; }
        
        [GameProperty("12")]
        public int OfficialSong { get; set; }
        
        [GameProperty("13")]
        public int GameVersion { get; set; }
        
        [GameProperty("14")]
        public int Likes { get; set; }
        
        [GameProperty("15")]
        public LengthType Length { get; set; }
        
        [GameProperty("17")]
        public bool Demon { get; set; }

        [GameProperty("18")]
        public int Stars { get; set; }
        
        [GameProperty("19")]
        public int FeatureScore { get; set; }
        
        [GameProperty("25")]
        public bool Auto { get; set; }
        
        [GameProperty("30")]
        public int CopiedId { get; set; }
        
        [GameProperty("31")]
        public bool TwoPlayer { get; set; }

        [GameProperty("35")]
        public int MusicId { get; set; }
        
        [GameProperty("37")]
        public int Coins { get; set; }
        
        [GameProperty("38")]
        public bool CoinsVerified { get; set; }
        
        [GameProperty("39")]
        public int StarsRequested { get; set; }
        
        [GameProperty("42")]
        public bool Epic { get; set; }
        
        [GameProperty("43")]
        public DemonDifficulty DemonDifficulty { get; set; }
        
        [GameProperty("45")]
        public int Objects { get; set; }
        
        [GameProperty("46")]
        public int? EditorTime { get; set; }
        
        [GameProperty("47")]
        public int? EditorTimeCopies { get; set; }
    }
}
