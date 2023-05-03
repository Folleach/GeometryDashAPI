using System;
using System.Collections.Generic;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels.GameObjects;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Levels
{
    public class Level
    {
        internal static IGameSerializer Serializer = new ObjectSerializer();
        
        public const string DefaultLevelString = "H4sIAAAAAAAAC6WQ0Q3CMAxEFwqSz4nbVHx1hg5wA3QFhgfn4K8VRfzci-34Kcq-1V7AZnTCg5UeQUBwQc3GGzgRZsaZICKj09iJBzgU5tcU-F-xHCryjhYuSZy5fyTK3_iI7JsmTjX2y2umE03ZV9RiiRAmoZVX6jyr80ZPbHUZlY-UYAzWNlJTmIBi9yfXQXYGDwIAAA==";

        public List<Color> Colors => Options.Colors;
        public BlockList Blocks { get; private set; }

        public int CountBlock => Blocks.Count;
        public int CountColor => Options.Colors.Count;

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
            Blocks = new BlockList();
        }

        public void AddBlock(IBlock block)
        {
            Blocks.Add(block);
        }

        public void AddColor(Color color)
        {
            Options.Colors.Add(color);
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

        public string SaveAsString(bool compress = true)
        {
            var builder = new StringBuilder();
            builder.Append(GeometryDashApi.Serializer.Encode(Options));

            foreach (var block in Blocks)
            {
                builder.Append(';');
                GeometryDashApi.Serializer.GetCopier(block.GetType()).Invoke(block, builder);
            }

            builder.Append(';');

            if (compress)
                return Compress(builder.ToString());
            return builder.ToString();
        }

        // see signatures https://en.wikipedia.org/wiki/List_of_file_signatures
        public static string Decompress(string data)
        {
            var bytes = GameConvert.FromBase64(data);
            if (bytes[0] == 0x78)
                return Crypt.ZLibDecompress(bytes);
            if (bytes[0] == 0x1F && bytes[1] == 0x8B)
                return Crypt.GZipDecompress(bytes);
            throw new InvalidOperationException(
                "Unsupported data signature. There is no gzip and zlib. Please check your level data. If your level is correct and works in the game, please create an issue: https://github.com/Folleach/GeometryDashAPI/issues. Or fix it yourself: https://github.com/Folleach/GeometryDashAPI/blob/master/GeometryDashAPI/Levels/Level.cs");
        }

        public static string Compress(string level)
        {
            return GameConvert.ToBase64(Crypt.GZipCompress(Encoding.UTF8.GetBytes(level)));
        }
    }
}
