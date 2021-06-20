using System.Collections.Generic;

namespace GeometryDashAPI.Server.Dtos
{
    public class LevelPageDto : GameStruct
    {
        [StructPosition(0)]
        [ArraySeparator("|")]
        public List<LevelPreviewDto> Levels { get; set; }
        
        [StructPosition(1)]
        [ArraySeparator("|")]
        public List<AuthorIdsDto> Authors { get; set; }
        
        [StructPosition(2)]
        [ArraySeparator("~:~")]
        public List<MusicInfoDto> Musics { get; set; }
        
        [StructPosition(3)]
        public Pagination Page { get; set; }
        
        [StructPosition(4)]
        public string Hash { get; set; }
        
        public override string GetParserSense() => "#";
    }
}