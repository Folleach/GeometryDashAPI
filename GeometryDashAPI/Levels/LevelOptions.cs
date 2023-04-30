using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels
{
    [Sense(",")]
    public class LevelOptions : GameObject
    {
        [GameProperty("kS38", KeyOverride = 0)]
        [ArraySeparator("|")]
        public List<Color> Colors { get; set; }

        [GameProperty("kA13", KeyOverride = 1)]
        public float MusicOffset { get; set; }
        
        [GameProperty("kA15", KeyOverride = 2)]
        public int Ka15 { get; set; }
        
        [GameProperty("kA16", KeyOverride = 3)]
        public int Ka16 { get; set; }
        
        [GameProperty("kA14", KeyOverride = 4)]
        public string Ka14 { get; set; }
        
        [GameProperty("kA6", KeyOverride = 5)]
        public short Background { get; set; }
        
        [GameProperty("kA7", KeyOverride = 6)]
        public short Ground { get; set; }
        
        [GameProperty("kA17", KeyOverride = 7)]
        public int Ka17 { get; set; }
        
        [GameProperty("kA18", KeyOverride = 8)]
        public short Font { get; set; }
        
        [GameProperty("kS39", KeyOverride = 9)]
        public int Ks39 { get; set; }
        
        [GameProperty("kA2", KeyOverride = 10)]
        public GameMode GameMode { get; set; }
        
        [GameProperty("kA3", KeyOverride = 11)]
        public bool IsMini { get; set; }
        
        [GameProperty("kA8", KeyOverride = 12)]
        public bool IsDual { get; set; }
        
        [GameProperty("kA4", KeyOverride = 13)]
        public SpeedType PlayerSpeed { get; set; }
        
        [GameProperty("kA9", KeyOverride = 14)]
        public int Ka9 { get; set; }
        
        [GameProperty("kA10", KeyOverride = 15)]
        public bool IsTwoPlayerMode { get; set; }
        
        [GameProperty("kA11", KeyOverride = 16)]
        public int Ka11 { get; set; }
    }
}
