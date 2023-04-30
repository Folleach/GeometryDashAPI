using System;
using System.Collections.Generic;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels.GameObjects;
using System.Text;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Serialization;
using Microsoft.Extensions.Primitives;

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

        public static string Decompress(string data)
        {
            if (data[0] == 'e' && data[1] == 'J')
                return Crypt.ZLibDecompress(GameConvert.FromBase64(data));
            return Crypt.GZipDecompress(GameConvert.FromBase64(data));
        }

        public static string Compress(string level)
        {
            return GameConvert.ToBase64(Crypt.GZipCompress(Encoding.UTF8.GetBytes(level)));
        }
    }
}
