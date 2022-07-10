using System;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Levels
{
    public class Level
    {
#if DEBUG
        public string LoadedString { get; private set; }
#endif
        internal static IGameParser parser = new ObjectParser();
        
        public const string DefaultLevelString = "H4sIAAAAAAAAC6WQ0Q3CMAxEFwqSz4nbVHx1hg5wA3QFhgfn4K8VRfzci-34Kcq-1V7AZnTCg5UeQUBwQc3GGzgRZsaZICKj09iJBzgU5tcU-F-xHCryjhYuSZy5fyTK3_iI7JsmTjX2y2umE03ZV9RiiRAmoZVX6jyr80ZPbHUZlY-UYAzWNlJTmIBi9yfXQXYGDwIAAA==";

        public ColorList Colors { get; private set; }
        public BlockList Blocks { get; private set; }

        public int CountBlock => Blocks.Count;
        public int CountColor => Colors.Count;

        public GameMode GameMode { get; set; } = GameMode.Cube;
        public SpeedType PlayerSpeed { get; set; } = SpeedType.Default;
        public bool Dual { get; set; }
        public bool TwoPlayerMode { get; set; }
        public bool Mini { get; set; }
        public byte Fonts { get; set; }
        public byte Background { get; set; }
        public byte Ground { get; set; }

        public float MusicOffset { get; set; }
        public int kA15 { get; set; }
        public int kA16 { get; set; }
        public string kA14 { get; set; }
        public int kA17 { get; set; }
        public int kS39 { get; set; }
        public int kA9 { get; set; }
        public int kA11 { get; set; }

        public Level()
        {
            Initialize();
            Load(DefaultLevelString, true);
        }

        public Level(string data, bool compressed)
        {
            Initialize();
            Load(data, compressed);
        }

        public Level(LevelCreatorModel model)
        {
            Initialize();
            Load(model.LevelString, true);
        }

        protected virtual void Initialize()
        {
            Colors = new ColorList();
            Blocks = new BlockList();
        }

        public void AddBlock(IBlock block)
        {
            Blocks.Add(block);
        }

        public void AddColor(Color color)
        {
            Colors.AddColor(color);
        }

        protected virtual void Load(string data, bool compressed)
        {
            if (compressed)
            {
                if (data[0] == 'e' && data[1] == 'J')
                    data = Crypt.ZLibDecompress(GameConvert.FromBase64(data));
                else
                    data = Crypt.GZipDecompress(GameConvert.FromBase64(data));
            }
#if DEBUG
            LoadedString = data;
#endif
            var parser = new LLParserSpan(";", data);
            var header = parser.Next();
            var headerParser = new LLParserSpan(",", header);
            while (headerParser.TryParseNext(out var key, out var value))
            {
                if (key.SequenceEqual("kS38"))
                    LoadColors(value);
                else if (key.SequenceEqual("kA13"))
                    MusicOffset = GameConvert.StringToSingle(value);
                else if (key.SequenceEqual("kA15"))
                    kA15 = int.Parse(value);
                else if (key.SequenceEqual("kA16"))
                    kA16 = int.Parse(value);
                else if (key.SequenceEqual("kA14"))
                    kA14 = value.ToString();
                else if (key.SequenceEqual("kA6"))
                    Background = byte.Parse(value);
                else if (key.SequenceEqual("kA7"))
                    Ground = byte.Parse(value);
                else if (key.SequenceEqual("kA17"))
                    kA17 = int.Parse(value);
                else if (key.SequenceEqual("kA18"))
                    Fonts = byte.Parse(value);
                else if (key.SequenceEqual("kS39"))
                    kS39 = int.Parse(value);
                else if (key.SequenceEqual("kA2"))
                    GameMode = (GameMode)int.Parse(value);
                else if (key.SequenceEqual("kA3"))
                    Mini = GameConvert.StringToBool(value, false);
                else if (key.SequenceEqual("kA8"))
                    Dual = GameConvert.StringToBool(value, false);
                else if (key.SequenceEqual("kA4"))
                    PlayerSpeed = (SpeedType)int.Parse(value);
                else if (key.SequenceEqual("kA9"))
                    kA9 = int.Parse(value);
                else if (key.SequenceEqual("kA10"))
                    TwoPlayerMode = GameConvert.StringToBool(value, false);
                else if (key.SequenceEqual("kA11"))
                    kA11 = int.Parse(value);
                else
                    throw new PropertyNotSupportedException(key.ToString(), value.ToString());
            }
            LoadBlocks(parser);
        }

        protected virtual void LoadColors(ReadOnlySpan<char> colorsData)
        {
            var parser = new LLParserSpan("|", colorsData);
            Span<char> color;
            while ((color = parser.Next()) != null && color.Length > 0)
                Colors.AddColor(new Color(color));
        }

        protected virtual void LoadBlocks(LLParserSpan llParser)
        {
            Span<char> rawBlock;
            while ((rawBlock = llParser.Next()) != null && rawBlock.Length > 0)
            {
                var block = parser.DecodeBlock(rawBlock);
                Blocks.Add((Block)block);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("kS38,");
            List<Color> colorList = Colors.ToList();
            foreach (Color color in colorList)
                builder.Append($"{color.ToString()}|");

            builder.Append($",kA13,{MusicOffset},kA15,{kA15},kA16,{kA16},kA14,{kA14},kA6,{Background}," +
                $"kA7,{Ground},kA17,{kA17},kA18,{Fonts},kS39,{kS39},kA2,{(byte)GameMode}," +
                $"kA3,{GameConvert.BoolToString(Mini)},kA8,{GameConvert.BoolToString(Dual)}," +
                $"kA4,{(byte)PlayerSpeed},kA9,{kA9},kA10,{GameConvert.BoolToString(TwoPlayerMode)},kA11,{kA11};");

            foreach (var block1 in Blocks)
            {
                var block = (Block)block1;
                throw new NotImplementedException("Encode temporary doesn't implemented");
                // builder.Append(parser.EncodeBlock(block));
                builder.Append(';');
            }

            byte[] bytes = Crypt.GZipCompress(Encoding.ASCII.GetBytes(builder.ToString()));
            return GameConvert.ToBase64(bytes);
        }
    }
}
