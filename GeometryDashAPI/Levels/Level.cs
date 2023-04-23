using System;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels.GameObjects;
using System.Collections.Generic;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Levels
{
    public class Level
    {
        internal static IGameSerializer Serializer = new ObjectSerializer();
        
        public const string DefaultLevelString = "H4sIAAAAAAAAC6WQ0Q3CMAxEFwqSz4nbVHx1hg5wA3QFhgfn4K8VRfzci-34Kcq-1V7AZnTCg5UeQUBwQc3GGzgRZsaZICKj09iJBzgU5tcU-F-xHCryjhYuSZy5fyTK3_iI7JsmTjX2y2umE03ZV9RiiRAmoZVX6jyr80ZPbHUZlY-UYAzWNlJTmIBi9yfXQXYGDwIAAA==";

        public ColorList Colors { get; private set; }
        public BlockList Blocks { get; private set; }

        public int CountBlock => Blocks.Count;
        public int CountColor => Colors.Count;

        public LevelOptions Options { get; set; }

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
                data = Decompress(data);
            var parser = new LLParserSpan(";", data);
            var header = parser.Next();
            Options = Level.Serializer.Decode<LevelOptions>(header);
            LoadBlocks(parser);
        }

        protected virtual void LoadBlocks(LLParserSpan llParser)
        {
            ReadOnlySpan<char> rawBlock;
            while ((rawBlock = llParser.Next()) != null && rawBlock.Length > 0)
            {
                var block = Serializer.DecodeBlock(rawBlock);
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

            // builder.Append($",kA13,{MusicOffset},kA15,{kA15},kA16,{kA16},kA14,{kA14},kA6,{Background}," +
            //     $"kA7,{Ground},kA17,{kA17},kA18,{Fonts},kS39,{kS39},kA2,{(byte)GameMode}," +
            //     $"kA3,{GameConvert.BoolToString(Mini)},kA8,{GameConvert.BoolToString(Dual)}," +
            //     $"kA4,{(byte)PlayerSpeed},kA9,{kA9},kA10,{GameConvert.BoolToString(TwoPlayerMode)},kA11,{kA11};");

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

        public static string Decompress(string data)
        {
            if (data[0] == 'e' && data[1] == 'J')
                return Crypt.ZLibDecompress(GameConvert.FromBase64(data));
            return Crypt.GZipDecompress(GameConvert.FromBase64(data));
        }
    }
}
