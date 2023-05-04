using System.Collections.Generic;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels
{
    [Sense(",")]
    public class LevelOptions : GameObject
    {
        [GameProperty("kS38", KeyOverride = 0)]
        [ArraySeparator("|", SeparatorAtTheEnd = true)]
        public List<Color> Colors { get; set; }

        [GameProperty("kA13", alwaysSet: true, KeyOverride = 1)]
        public float MusicOffset { get; set; }

        [GameProperty("kA15", alwaysSet: true, KeyOverride = 2)]
        public int Ka15 { get; set; }

        [GameProperty("kA16", alwaysSet: true, KeyOverride = 3)]
        public int Ka16 { get; set; }

        [GameProperty("kA14", KeyOverride = 4)]
        public string Ka14 { get; set; }

        [GameProperty("kA6", alwaysSet: true, KeyOverride = 5)]
        public short Background { get; set; }

        [GameProperty("kA7", alwaysSet: true, KeyOverride = 6)]
        public short Ground { get; set; }

        [GameProperty("kA17", alwaysSet: true, KeyOverride = 7)]
        public int Ka17 { get; set; }

        [GameProperty("kA18", alwaysSet: true, KeyOverride = 8)]
        public short Font { get; set; }

        [GameProperty("kS39", alwaysSet: true, KeyOverride = 9)]
        public int Ks39 { get; set; }

        [GameProperty("kA2", alwaysSet: true, KeyOverride = 10)]
        public GameMode GameMode { get; set; }

        [GameProperty("kA3", alwaysSet: true, KeyOverride = 11)]
        public bool IsMini { get; set; }

        [GameProperty("kA8", alwaysSet: true, KeyOverride = 12)]
        public bool IsDual { get; set; }

        [GameProperty("kA4", alwaysSet: true, KeyOverride = 13)]
        public SpeedType PlayerSpeed { get; set; }

        [GameProperty("kA9", alwaysSet: true, KeyOverride = 14)]
        public int Ka9 { get; set; }

        [GameProperty("kA10", alwaysSet: true, KeyOverride = 15)]
        public bool IsTwoPlayerMode { get; set; }

        [GameProperty("kA11", alwaysSet: true, KeyOverride = 16)]
        public int Ka11 { get; set; }

        [GameProperty("kA1", KeyOverride = 17)]
        public int Ka1 { get; set; }

        [GameProperty("kS1", KeyOverride = 18)]
        public int Ks1 { get; set; }

        [GameProperty("kS2", KeyOverride = 19)]
        public int Ks2 { get; set; }

        [GameProperty("kS3", KeyOverride = 20)]
        public int Ks3 { get; set; }

        [GameProperty("kS4", KeyOverride = 21)]
        public int Ks4 { get; set; }

        [GameProperty("kS5", KeyOverride = 22)]
        public int Ks5 { get; set; }

        [GameProperty("kS6", KeyOverride = 23)]
        public int Ks6 { get; set; }
    }
}
