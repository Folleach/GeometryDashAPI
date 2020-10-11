using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public class Level
    {
#if DEBUG
        [Obsolete("Don't use it. Thank you :3")]
        public string LoadedString { get; private set; }
#endif

        public const string DefaultLevelString = "H4sIAAAAAAAAC6WQ0Q3CMAxEFwqSz4nbVHx1hg5wA3QFhgfn4K8VRfzci-34Kcq-1V7AZnTCg5UeQUBwQc3GGzgRZsaZICKj09iJBzgU5tcU-F-xHCryjhYuSZy5fyTK3_iI7JsmTjX2y2umE03ZV9RiiRAmoZVX6jyr80ZPbHUZlY-UYAzWNlJTmIBi9yfXQXYGDwIAAA==";
        private static LevelParser defaultParser;

        public ColorList Colors { get; private set; }
        public BlockList Blocks { get; private set; }

        #region Properties
        public int CountBlock => Blocks.Count;
        public int CountColor => Colors.Count;
        #endregion

        #region Level properties
        [GameProperty("kA2", 0, true)] public GameMode GameMode { get; set; } = GameMode.Cube;
        [GameProperty("kA4", 0, true)] public SpeedType PlayerSpeed { get; set; } = SpeedType.Default;
        [GameProperty("kA8", 0, true)] public bool Dual { get; set; }
        [GameProperty("kA10", 0, true)] public bool TwoPlayerMode { get; set; }
        [GameProperty("kA3", 0, true)] public bool Mini { get; set; }
        [GameProperty("kA18", 0, true)] public byte Font { get; set; }
        [GameProperty("kA6", 0, true)] public byte Background { get; set; }
        [GameProperty("kA7", 0, true)] public byte Ground { get; set; }
        [GameProperty("kA13", 0, true)] public float MusicOffset { get; set; }
        [GameProperty("kA15", 0, true)] public int kA15 { get; set; }
        [GameProperty("kA16", 0, true)] public int kA16 { get; set; }
        [GameProperty("kA14", 0, true)] public string kA14 { get; set; }
        [GameProperty("kA17", 0, true)] public int kA17 { get; set; }
        [GameProperty("kS39", 0, true)] public int kS39 { get; set; }
        [GameProperty("kA9", 0, true)] public int kA9 { get; set; }
        [GameProperty("kA11", 0, true)] public int kA11 { get; set; }
        #endregion

        #region Constructor
        public Level()
        {
        }

        public Level(string data, LevelParser parser = null)
        {
            Colors = new ColorList();
            Blocks = new BlockList();
            // :thinking:
            (parser ?? (defaultParser ??= new LevelParser(new TypeMapping()))).Parse(data, this);
        }

        public Level(LevelCreatorModel model, LevelParser parser = null) : this(model.LevelString, parser)
        {
        }
        #endregion

        public void AddBlock(IBlock block)
        {
            this.Blocks.Add(block);
        }

        public void AddColor(Color color)
        {
            this.Colors.AddColor(color);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("kS38,");
            List<Color> colorList = Colors.ToList();
            foreach (Color color in colorList)
                builder.Append($"{color.ToString()}|");

            builder.Append($",kA13,{MusicOffset},kA15,{kA15},kA16,{kA16},kA14,{kA14},kA6,{Background}," +
                $"kA7,{Ground},kA17,{kA17},kA18,{Font},kS39,{kS39},kA2,{(byte)GameMode}," +
                $"kA3,{GameConvert.BoolToString(Mini)},kA8,{GameConvert.BoolToString(Dual)}," +
                $"kA4,{(byte)PlayerSpeed},kA9,{kA9},kA10,{GameConvert.BoolToString(TwoPlayerMode)},kA11,{kA11};");

            foreach (IBlock block in Blocks)
            {
                builder.Append(block.ToString());
                builder.Append(';');
            }

            byte[] bytes = Crypt.GZipCompress(Encoding.ASCII.GetBytes(builder.ToString()));
            return GameConvert.ToBase64(bytes);
        }
    }
}
