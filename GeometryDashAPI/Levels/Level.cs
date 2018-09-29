using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GeometryDashAPI.Levels
{
    public class Level
    {
        //TODO: Temp property
        public string stringData { get; set; }

        public ColorList Colors { get; private set; }
        public BlockList Blocks { get; private set; }

        public Mode GameMode { get; set; } = Mode.Cube;
        public Speed PlayerSpeed { get; set; } = Speed.Default;
        public bool Dual { get; set; }
        public bool TwoPlayerMode { get; set; }
        public bool Mini { get; set; }
        public byte Fonts { get; set; }
        public byte Background { get; set; }
        public byte Ground { get; set; }

        public int kA13 { get; set; }
        public int kA15 { get; set; }
        public int kA16 { get; set; }
        public string kA14 { get; set; }
        public int kA17 { get; set; }
        public int kS39 { get; set; }
        public int kA9 { get; set; }
        public int kA11 { get; set; }

        public Level()
        {
            this.Initialize();
        }

        public Level(LevelCreatorModel model)
        {
            this.Initialize();
            this.Load(model.LevelString);
        }

        protected virtual void Initialize()
        {
            Colors = new ColorList();
            Blocks = new BlockList();
        }

        protected virtual void Load(string compressData)
        {
            string data = Crypt.GZipDecompress(GameConvert.FromBase64(compressData));
            stringData = data;
            string[] splitData = data.Split(';');
            string[] levelProperties = splitData[0].Split(',');
            for (int i = 0; i < levelProperties.Length; i += 2)
            {
                switch (levelProperties[i])
                {
                    case "kS38":
                        this.LoadColors(levelProperties[i + 1]);
                        break;
                    case "kA13":
                        kA13 = int.Parse(levelProperties[i + 1]);
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
                        GameMode = (Mode)int.Parse(levelProperties[i + 1]);
                        break;
                    case "kA3":
                        Mini = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA8":
                        Dual = GameConvert.StringToBool(levelProperties[i + 1], false);
                        break;
                    case "kA4":
                        PlayerSpeed = (Speed)int.Parse(levelProperties[i + 1]);
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
                        throw new Exception(ExceptionMessages.PropertyNotSupported(levelProperties[i], levelProperties[i + 1]));
                }
            }
            this.LoadBlocks(splitData);
            GC.Collect();
        }

        protected virtual void LoadColors(string colorsData)
        {
            foreach (string colorData in colorsData.Split('|'))
            {
                if (colorData == string.Empty)
                    continue;

                this.Colors.AddColor(new Color(colorData));
            }
        }

        protected virtual void LoadBlocks(string[] blocksData)
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Restart();
#endif
            for (int i = 1; i < blocksData.Length - 1; i++)
            {
                string[] block = blocksData[i].Split(',');
                int j = BlockTypeID.GetTypeByID(block[1]);

                switch (j)
                {
                    case 1:
                        Blocks.Add(new Block(block));
                        break;
                }
            }
#if DEBUG
            sw.Stop();
            Debug.WriteLine($"Block load time: {sw.ElapsedTicks} ticks");
            Debug.WriteLine($"Block load time: {sw.ElapsedMilliseconds} milliseconds");
#endif
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("kS38,");
            foreach (KeyValuePair<short, Color> color in Colors)
            {
                builder.Append($"{color.Value.ToString()}|");
            }
            builder.Append($",kA13,{kA13},kA15,{kA15},kA16,{kA16},kA14,{kA14},kA6,{Background}," +
                $"kA7,{Ground},kA17,{kA17},kA18,{Fonts},kS39,{kS39},kA2,{(byte)GameMode}," +
                $"kA3,{GameConvert.BoolToString(Mini)},kA8,{GameConvert.BoolToString(Dual)}," +
                $"kA4,{(byte)PlayerSpeed},kA9,{kA9},kA10,{GameConvert.BoolToString(TwoPlayerMode)},kA11,{kA11};");
            foreach (var element in Blocks)
            {
                if (element is Block)
                    builder.Append($"{(element as Block).ToString()};");
            }
            byte[] bytes = Crypt.GZipCompress(Encoding.ASCII.GetBytes(builder.ToString()));
            return GameConvert.ToBase64(bytes);
        }
    }
}
