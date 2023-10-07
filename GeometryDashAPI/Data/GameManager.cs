using System;
using System.Buffers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Data.Classes;
using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data
{
    public class GameManager : GameData
    {
        public string PlayerName
        {
            get => DataPlist.TryGetValue("playerName", out var value) ? value : 0;
            set => DataPlist["playerName"] = value;
        }
        public string PlayerUdid
        {
            get => DataPlist.TryGetValue("playerUDID", out var value) ? value : 0;
            set => DataPlist["playerUDID"] = value;
        }
        public int PlayerId
        {
            get => DataPlist.TryGetValue("playerUserID", out var value) ? value : 0;
            set => DataPlist["playerUserID"] = value;
        }
        public int Bootups
        {
            get => DataPlist.TryGetValue("bootups", out var value) ? value : 0;
            set => DataPlist["bootups"] = value;
        }
        public float MusicVolume
        {
            get => DataPlist.TryGetValue("bgVolume", out var value) ? value : 0;
            set => DataPlist["bgVolume"] = value;
        }
        public float SfxVolume
        {
            get => DataPlist.TryGetValue("sfxVolume", out var value) ? value : 0;
            set => DataPlist["sfxVolume"] = value;
        }
        public int PlayerCube
        {
            get => DataPlist.TryGetValue("playerFrame", out var value) ? value : 0;
            set => DataPlist["playerFrame"] = value;
        }
        public int PlayerShip
        {
            get => DataPlist.TryGetValue("playerShip", out var value) ? value : 0;
            set => DataPlist["playerShip"] = value;
        }
        public int PlayerBall
        {
            get => DataPlist.TryGetValue("playerBall", out var value) ? value : 0;
            set => DataPlist["playerBall"] = value;
        }
        public int PlayerBird
        {
            get => DataPlist.TryGetValue("playerBird", out var value) ? value : 0;
            set => DataPlist["playerBird"] = value;
        }
        public int PlayerWave
        {
            get => DataPlist.TryGetValue("playerDart", out var value) ? value : 0;
            set => DataPlist["playerDart"] = value;
        }
        public int PlayerRobot
        {
            get => DataPlist.TryGetValue("playerRobot", out var value) ? value : 0;
            set => DataPlist["playerRobot"] = value;
        }
        public int PlayerSpider
        {
            get => DataPlist.TryGetValue("playerSpider", out var value) ? value : 0;
            set => DataPlist["playerSpider"] = value;
        }
        public int PlayerColor1
        {
            get => DataPlist.TryGetValue("playerColor", out var value) ? value : 0;
            set => DataPlist["playerColor"] = value;
        }
        public int PlayerColor2
        {
            get => DataPlist.TryGetValue("playerColor2", out var value) ? value : 0;
            set => DataPlist["playerColor2"] = value;
        }
        public int PlayerStreak
        {
            get => DataPlist.TryGetValue("playerStreak", out var value) ? value : 0;
            set => DataPlist["playerStreak"] = value;
        }
        public int PlayerDeathEffect
        {
            get => DataPlist.TryGetValue("playerDeathEffect", out var value) ? value : 0;
            set => DataPlist["playerDeathEffect"] = value;
        }
        public bool ShowSongMarkers
        {
            get => DataPlist.TryGetValue("showSongMarkers", out var value) ? value : false;
            set => DataPlist["showSongMarkers"] = value;
        }
        public int BinaryVersion
        {
            get => DataPlist.TryGetValue("binaryVersion", out var value) ? value : 0;
            set => DataPlist["binaryVersion"] = value;
        }
        //TODO: Move to value keeper
        public bool FullScreen
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0025", out var value) ? GameConvert.StringToBool(value, isReverse: true) : false;
            set => DataPlist["valueKeeper"]["gv_0025"] = GameConvert.BoolToString(value, isReverse: true);
        }
        public TextureQuality TextureQuality
        {
            get => DataPlist.TryGetValue("texQuality", out var value) ? (TextureQuality)value : TextureQuality.Auto;
            set
            {
                if (value == TextureQuality.Auto)
                    DataPlist.Remove("texQuality");
                else
                    DataPlist["texQuality"] = (int)value;
            }
        }

        //  ** by lex **
        public bool PlayerGlow
        {
            get => DataPlist.TryGetValue("playerGlow", out var value);
            set
            {
                if (value == false)
                    DataPlist.Remove("playerGlow");
                else
                    DataPlist["playerGlow"] = value;
            }
        }
        public bool SmoothFix
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0023", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0023"] = GameConvert.BoolToString(value);
        }
        public bool VerticalSync
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0030", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0030"] = GameConvert.BoolToString(value);
        }
        public GDResolution Resolution
        {
            get => DataPlist.TryGetValue("resolution", out var value) ? GDResolution.ToGDResolution(value) : new GDResolution(0,0);
            set => DataPlist["resolution"] = GDResolution.FromGDResolution(value);
        }
        public int RawResolution
        {
            get => DataPlist.TryGetValue("resolution", out var value) ? (int)value : 0;
            set => DataPlist["resolution"] = value;
        }
        public bool LowDetailMode
        {
            get => DataPlist.TryGetValue("performanceMode", out _);
            set
            {
                if (value == false)
                    DataPlist.Remove("performanceMode");
                else
                    DataPlist["performanceMode"] = value;
            }
        }
        public bool AutoRetry
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0026", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0026"] = GameConvert.BoolToString(value);
        }
        public bool LoadSongsToMemory
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0019", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0019"] = GameConvert.BoolToString(value);
        }
        public bool HighStartPosAccuracy
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0067", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0067"] = GameConvert.BoolToString(value);
        }
        public bool ShowRestartButton
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0074", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0074"] = GameConvert.BoolToString(value);
        }
        public bool ChangeCustomSongsLocation
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0033", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0033"] = GameConvert.BoolToString(value);
        }
        public bool AutoCheckpoints
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0027", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0027"] = GameConvert.BoolToString(value);
        }
        public bool HighCapacityMode
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0066", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0066"] = GameConvert.BoolToString(value);
        }
        public bool QuickCheckpointMode
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0068", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0068"] = GameConvert.BoolToString(value);
        }
        public bool ForceSmoothFix
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0101", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0101"] = GameConvert.BoolToString(value);
        }
        public bool DisableExplosionShake
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0014", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0014"] = GameConvert.BoolToString(value);
        }
        public bool DisableShakeEffect
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0081", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0081"] = GameConvert.BoolToString(value);
        }
        public bool AutoLoadComments
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0090", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0090"] = GameConvert.BoolToString(value);
        }

        // [GameManagerProperty(type: typeof(bool), defaultValue: false, path: "valueKeeper,gv_0040")] = example of potential attribute to be implemented in the future..
        public bool ShowPercentage
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0040", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0040"] = GameConvert.BoolToString(value);
        }
        public bool IncreaseLocalLevelsPerPage
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0093", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0093"] = GameConvert.BoolToString(value);
        }
        public bool NewCompletedFilter
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0073", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0073"] = GameConvert.BoolToString(value);
        }
        public bool MoreCommentsMode
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0094", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0094"] = GameConvert.BoolToString(value);
        }
        public bool FlipPauseButton
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0015", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0015"] = GameConvert.BoolToString(value);
        }
        public bool IncreaseMaxLevels
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0042", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0042"] = GameConvert.BoolToString(value);
        }
        public bool FastPracticeReset
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0052", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0052"] = GameConvert.BoolToString(value);
        }
        public bool PracticeDeathEffect
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0100", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0100"] = GameConvert.BoolToString(value);
        }
        public bool HidePracticeButtons
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0071", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0071"] = GameConvert.BoolToString(value);
        }
        public bool DisableSongAlert
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0083", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0083"] = GameConvert.BoolToString(value);
        }
        public bool ShowLeaderboardPercent
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0099", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0099"] = GameConvert.BoolToString(value);
        }
        public bool DefaultMiniIcon
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0060", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0060"] = GameConvert.BoolToString(value);
        }
        public bool SwitchDashFireColor
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0062", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0062"] = GameConvert.BoolToString(value);
        }
        public bool DisableHighObjectAlert
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0082", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0082"] = GameConvert.BoolToString(value);
        }
        public bool ManualOrder
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0084", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0084"] = GameConvert.BoolToString(value);
        }
        public bool SmoothFixInEditor
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0102", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0102"] = GameConvert.BoolToString(value);
        }
        public bool DisableThumbstick
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0028", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0028"] = GameConvert.BoolToString(value);
        }
        public bool NoSongLimit
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0018", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0018"] = GameConvert.BoolToString(value);
        }
        public bool Flip2PlayerControls
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0010", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0010"] = GameConvert.BoolToString(value);
        }
        public bool JustDont
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0095", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0095"] = GameConvert.BoolToString(value);
        }
        public bool EditorHoldToSwipe
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0057", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0057"] = GameConvert.BoolToString(value);
        }
        public bool ShowCursorInGame
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0024", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0024"] = GameConvert.BoolToString(value);
        }
        public bool AlwaysLimitControls
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0011", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0011"] = GameConvert.BoolToString(value);
        }
        public bool DisableGravityEffect
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0072", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0072"] = GameConvert.BoolToString(value);
        }
        public bool EnableMoveOptimization
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0065", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0065"] = GameConvert.BoolToString(value);
        }
        public bool IncreasedMaxUndoRedo
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0013", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0013"] = GameConvert.BoolToString(value);
        }
        public bool SwipeCycleMode
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0059", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0059"] = GameConvert.BoolToString(value);
        }
        public bool SwitchSpiderTeleportColor
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0061", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0061"] = GameConvert.BoolToString(value);
        }
        public bool SwitchWaveTrailColor
        {
            get => ((Plist)DataPlist["valueKeeper"]).TryGetValue("gv_0096", out var value) ? GameConvert.StringToBool(value) : false;
            set => DataPlist["valueKeeper"]["gv_0096"] = GameConvert.BoolToString(value);
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
