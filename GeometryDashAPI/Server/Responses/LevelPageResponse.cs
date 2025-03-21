﻿using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("#")]
    [AsStruct]
    public class LevelPageResponse : GameObject
    {
        [GameProperty("0")]
        [ArraySeparator("|")]
        public List<LevelPreview> Levels { get; set; }
        
        [GameProperty("1")]
        [ArraySeparator("|")]
        public List<AuthorIds> Authors { get; set; }
        
        [GameProperty("2")]
        [ArraySeparator("~:~")]
        public List<MusicInfo> Musics { get; set; }
        
        [GameProperty("3")]
        public Pagination Page { get; set; }
        
        [GameProperty("4")]
        public string Hash { get; set; }
    }
}