using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using System.Collections.Generic;
using System.IO;
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

        public const string DefaultLevelString = "H4sIAAAAAAAAC6WQ0Q3CMAxEFwqSz4nbVHx1hg5wA3QFhgfn4K8VRfzci-34Kcq-1V7AZnTCg5UeQUBwQc3GGzgRZsaZICKj09iJBzgU5tcU-F-xHCryjhYuSZy5fyTK3_iI7JsmTjX2y2umE03ZV9RiiRAmoZVX6jyr80ZPbHUZlY-UYAzWNlJTmIBi9yfXQXYGDwIAAA==";

        public ColorList Colors { get; private set; }
        public BlockList Blocks { get; private set; }

        public int CountBlock { get => Blocks.Count; }
        public int CountColor { get => Colors.Count; }

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
                    data = Crypt.ZlipDecompress(GameConvert.FromBase64(data));
                else
                    data = Crypt.GZipDecompress(GameConvert.FromBase64(data));
            }
#if DEBUG
            LoadedString = data;
#endif
            string[] splitData = data.Split(';');
            string[] levelProperties = splitData[0].Split(',');
            for (int i = 0; i < levelProperties.Length; i += 2)
            {
                switch (levelProperties[i])
                {
                    case "kS38":
                        LoadColors(levelProperties[i + 1]);
                        break;
                    case "kA13":
                        MusicOffset = GameConvert.StringToSingle(levelProperties[i + 1]);
                        break;
                    case "kA15":
                        kA15 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA16":
                        kA16 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA14":
                        kA14 = levelProperties[i + 1];
                        break;
                    case "kA6":
                        Background = byte.Parse(levelProperties[i + 1]);
                        break;
                    case "kA7":
                        Ground = byte.Parse(levelProperties[i + 1]);
                        break;
                    case "kA17":
                        kA17 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA18":
                        Fonts = byte.Parse(levelProperties[i + 1]);
                        break;
                    case "kS39":
                        kS39 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA2":
                        GameMode = (GameMode)int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA3":
                        Mini = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA8":
                        Dual = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA4":
                        PlayerSpeed = (SpeedType)int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA9":
                        kA9 = int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA10":
                        TwoPlayerMode = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA11":
                        kA11 = int.Parse(levelProperties[i + 1]);
                        break;
                    default:
                        //throw new PropertyNotSupportedException(levelProperties[i], levelProperties[i + 1]);
                        break;
                }
            }
            LoadBlocks(splitData);
        }

        protected virtual void LoadColors(string colorsData)
        {
            foreach (string colorData in colorsData.Split('|'))
            {
                if (colorData == string.Empty)
                    continue;
                Colors.AddColor(new Color(colorData));
            }
        }

        protected virtual void LoadBlocks(string[] blocksData)
        {
            for (var i = 1; i < blocksData.Length - 1; i++)
            {
                var block = ObjectParser.DecodeBlock(blocksData[i]);
                Blocks.Add(block);
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

            foreach (Block block in Blocks)
            {
                builder.Append(ObjectParser.EncodeBlock(block));
                builder.Append(';');
            }

            byte[] bytes = Crypt.GZipCompress(Encoding.ASCII.GetBytes(builder.ToString()));
            return GameConvert.ToBase64(bytes);
        }
    }
}
