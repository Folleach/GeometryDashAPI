using System;
using System.Buffers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data
{
    public class GameManager : GameData
    {
        public string PlayerName
        {
            get => DataPlist["playerName"];
            set => DataPlist["playerName"] = value;
        }
        public string PlayerUdid
        {
            get => DataPlist["playerUDID"];
            set => DataPlist["playerUDID"] = value;
        }
        public int PlayerId
        {
            get => DataPlist.ContainsKey("playerUserID") ? DataPlist["playerUserID"] : 0;
            set => DataPlist["playerUserID"] = value;
        }
        public int Bootups
        {
            get => DataPlist["bootups"];
            set => DataPlist["bootups"] = value;
        }
        //TODO: Move to value keeper
        public bool FullScreen
        {
            get => DataPlist["valueKeeper"]["gv_0025"] == "0" ? true : false;
            set => DataPlist["valueKeeper"]["gv_0025"] = GameConvert.BoolToString(value, true);
        }
        public float MusicVolume
        {
            get => DataPlist["bgVolume"];
            set => DataPlist["bgVolume"] = value;
        }
        public float SongEffectVolume
        {
            get => DataPlist["sfxVolume"];
            set => DataPlist["sfxVolume"] = value;
        }
        public int PlayerCube
        {
            get => DataPlist["playerFrame"];
            set => DataPlist["playerFrame"] = value;
        }
        public int PlayerShip
        {
            get => DataPlist["playerShip"];
            set => DataPlist["playerShip"] = value;
        }
        public int PlayerBall
        {
            get => DataPlist["playerBall"];
            set => DataPlist["playerBall"] = value;
        }
        public int PlayerBird
        {
            get => DataPlist["playerBird"];
            set => DataPlist["playerBird"] = value;
        }
        public int PlayerWave
        {
            get => DataPlist["playerDart"];
            set => DataPlist["playerDart"] = value;
        }
        public int PlayerRobot
        {
            get => DataPlist["playerRobot"];
            set => DataPlist["playerRobot"] = value;
        }
        public int PlayerSpider
        {
            get => DataPlist["playerSpider"];
            set => DataPlist["playerSpider"] = value;
        }
        public int PlayerColor1
        {
            get => DataPlist.ContainsKey("playerColor") ? DataPlist["playerColor"] : 0;
            set => DataPlist["playerColor"] = value;
        }
        public int PlayerColor2
        {
            get => DataPlist.ContainsKey("playerColor2") ? DataPlist["playerColor2"] : 0;
            set => DataPlist["playerColor2"] = value;
        }
        public int PlayerStreak
        {
            get => DataPlist["playerStreak"];
            set => DataPlist["playerStreak"] = value;
        }
        public int PlayerDeathEffect
        {
            get => DataPlist["playerDeathEffect"];
            set => DataPlist["playerDeathEffect"] = value;
        }
        public bool ShowSongMarkers
        {
            get => DataPlist.TryGetValue("showSongMarkers", out var value) ? value : false;
            set => DataPlist["showSongMarkers"] = value;
        }
        public int BinaryVersion
        {
            get => DataPlist["binaryVersion"];
            set => DataPlist["binaryVersion"] = value;
        }
        public TextureQuality TextureQuality
        {
            get
            {
                if (DataPlist.ContainsKey("texQuality"))
                    return (TextureQuality)DataPlist["texQuality"];
                else
                    return TextureQuality.Auto;
            }
            set
            {
                if (value == TextureQuality.Auto)
                    DataPlist.Remove("texQuality");
                else
                    DataPlist["texQuality"] = (int)value;
            }
        }

        protected GameManager() : base(GameDataType.GameManager)
        {
        }

        public static async Task<GameManager> LoadFileAsync(string? fileName = null)
        {
            var local = new GameManager();
            await local.LoadAsync(fileName ?? ResolveFileName(GameDataType.GameManager));
            return local;
        }

        public static GameManager LoadFile(string? fileName = null)
        {
            var local = new GameManager();
            local.LoadAsync(fileName ?? ResolveFileName(GameDataType.GameManager)).GetAwaiter().GetResult();
            return local;
        }

        public static GameManager CreateNew()
        {
            var manager = new GameManager
            {
                DataPlist = new Plist
                {
                    ["valueKeeper"] = new Plist
                    {
                        ["gv_0002"] = "1",
                        ["gv_0001"] = "1",
                        ["gv_0026"] = "1",
                        ["gv_0027"] = "1",
                        ["gv_0023"] = "1",
                        ["gv_0038"] = "1",
                        ["gv_0043"] = "1",
                        ["gv_0044"] = "1",
                        ["gv_0050"] = "2",
                        ["gv_0049"] = "6",
                        ["gv_0046"] = "1",
                        ["gv_0036"] = "1",
                        ["gv_0030"] = "1",
                        ["gv_0019"] = "1",
                        ["gv_0013"] = "1",
                        ["gv_0018"] = "1"
                    },
                    ["unlockValueKeeper"] = new Plist(),
                    ["customObjectDict"] = new Plist(),
                    ["bgVolume"] = 1f,
                    ["sfxVolume"] = 1f,
                    ["reportedAchievements"] = new Plist(),
                    ["GLM_01"] = new Plist(),
                    ["GLM_03"] = new Plist(),
                    ["GLM_06"] = new Plist(),
                    ["GLM_07"] = new Plist(),
                    ["GLM_08"] = new Plist(),
                    ["GLM_09"] = new Plist(),
                    ["GLM_10"] = new Plist(),
                    ["GLM_12"] = new Plist(),
                    ["GLM_13"] = new Plist(),
                    ["GLM_14"] = new Plist(),
                    ["GLM_15"] = new Plist(),
                    ["GLM_16"] = new Plist(),
                    ["GLM_18"] = new Plist(),
                    ["GLM_19"] = new Plist(),
                    ["GS_value"] = new Plist()
                    {
                        ["1"] = "0",
                        ["2"] = "0",
                        ["3"] = "0",
                        ["4"] = "0",
                        ["5"] = "0",
                        ["6"] = "0",
                        ["7"] = "0",
                        ["8"] = "0",
                        ["9"] = "0",
                        ["10"] =  "0",
                        ["11"] =  "0",
                        ["12"] =  "0",
                        ["13"] =  "0",
                        ["14"] =  "0",
                        ["15"] =  "0",
                        ["16"] =  "0",
                        ["17"] =  "0",
                        ["18"] =  "0",
                        ["19"] =  "0",
                        ["20"] =  "0",
                        ["21"] =  "0",
                        ["22"] =  "0"
                    },
                    ["GS_completed"] = new Plist(),
                    ["GS_3"] = new Plist(),
                    ["GS_4"] = new Plist(),
                    ["GS_5"] = new Plist(),
                    ["GS_6"] = new Plist(),
                    ["GS_7"] = new Plist(),
                    ["GS_8"] = new Plist(),
                    ["GS_9"] = new Plist(),
                    ["GS_10"] = new Plist(),
                    ["GS_11"] = new Plist(),
                    ["GS_12"] = new Plist(),
                    ["GS_14"] = new Plist(),
                    ["GS_15"] = new Plist(),
                    ["GS_16"] = new Plist(),
                    ["GS_17"] = new Plist(),
                    ["GS_18"] = new Plist(),
                    ["GS_19"] = new Plist(),
                    ["GS_21"] = new Plist(),
                    ["GS_22"] = new Plist(),
                    ["GS_23"] = new Plist(),
                    ["GS_24"] = new Plist(),
                    ["GS_25"] = new Plist(),
                    ["MDLM_001"] = new Plist(),
                    ["KBM_001"] = new Plist(),
                    ["KBM_002"] = new Plist(),
                    ["resolution"] = -1
                },
                PlayerName = "Player",
                PlayerUdid = GenerateUdid(),
                PlayerCube = 1,
                PlayerShip = 1,
                PlayerBall = 1,
                PlayerBird = 1,
                PlayerWave = 1,
                PlayerRobot = 1,
                PlayerSpider = 1,
                PlayerColor2 = 3,
                PlayerStreak = 1,
                PlayerDeathEffect = 1,
                ShowSongMarkers = true,
                BinaryVersion = GeometryDashApi.BinaryVersion
            };
            return manager;
        }

        // I don't know about how RobTop use this Udid and have no idea how to generate it
        // therefore, I will generate cryptographically strong random values.
        // Maybe it's device identifier https://en.wikipedia.org/wiki/UDID
        private static string GenerateUdid()
        {
            var builder = new StringBuilder();
            builder.Append("S");
#if !NETSTANDARD2_1
            var c = RandomNumberGenerator.Create();
#endif

            for (var i = 0; i < 6; i++)
#if NETSTANDARD2_1
                builder.Append(RandomNumberGenerator.GetInt32(0, 999999).ToString("000000"));
            builder.Append(RandomNumberGenerator.GetInt32(0, 9));
#else
            {
                var buffer = ArrayPool<byte>.Shared.Rent(4);
                c.GetBytes(buffer, 0, 4);
                builder.Append((Math.Abs(BitConverter.ToInt32(buffer, 0)) % 1000000).ToString("000000"));
                ArrayPool<byte>.Shared.Return(buffer);
            }
            var buffer2 = ArrayPool<byte>.Shared.Rent(1);
            c.GetBytes(buffer2, 0, 4);
            builder.Append((Math.Abs(BitConverter.ToInt32(buffer2, 0)) % 10).ToString("000000"));
            ArrayPool<byte>.Shared.Return(buffer2);
#endif
            return builder.ToString();
        }
    }
}
