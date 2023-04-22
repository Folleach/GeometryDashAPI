using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels
{
    [Sense(",")]
    public class LevelOptions : IGameObject
    {
        [GameProperty("kS38", keyOverride: 0)]
        [ArraySeparator("|")]
        public List<Color> Colors { get; set; }

        [GameProperty("kA13", keyOverride: 1)]
        public float MusicOffset { get; set; }
        
        [GameProperty("kA15", keyOverride: 2)]
        public int Ka15 { get; set; }
        
        [GameProperty("kA16", keyOverride: 3)]
        public int Ka16 { get; set; }
        
        [GameProperty("kA14", keyOverride: 4)]
        public string Ka14 { get; set; }
        
        [GameProperty("kA6", keyOverride: 5)]
        public short Background { get; set; }
        
        [GameProperty("kA7", keyOverride: 6)]
        public short Ground { get; set; }
        
        [GameProperty("kA17", keyOverride: 7)]
        public int Ka17 { get; set; }
        
        [GameProperty("kA18", keyOverride: 8)]
        public short Font { get; set; }
        
        [GameProperty("kS39", keyOverride: 9)]
        public int Ks39 { get; set; }
        
        [GameProperty("kA2", keyOverride: 10)]
        public GameMode GameMode { get; set; }
        
        [GameProperty("kA3", keyOverride: 11)]
        public bool IsMini { get; set; }
        
        [GameProperty("kA8", keyOverride: 12)]
        public bool IsDual { get; set; }
        
        [GameProperty("kA4", keyOverride: 13)]
        public SpeedType PlayerSpeed { get; set; }
        
        [GameProperty("kA9", keyOverride: 14)]
        public int Ka9 { get; set; }
        
        [GameProperty("kA10", keyOverride: 15)]
        public bool IsTwoPlayerMode { get; set; }
        
        [GameProperty("kA11", keyOverride: 16)]
        public int Ka11 { get; set; }

        public List<string> WithoutLoaded { get; }
    }
}
