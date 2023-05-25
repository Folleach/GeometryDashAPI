using System.IO;
using System.Threading.Tasks;
using GeometryDashAPI.Data.Enums;

namespace GeometryDashAPI.Data
{
    public class GameManager : GameData
    {
        public string PlayerName
        {
            get => DataPlist["playerName"];
            set => DataPlist["playerName"] = value;
        }
        public string PlayerUDID
        {
            get => DataPlist["playerUDID"];
            set => DataPlist["playerUDID"] = value;
        }
        public int PlayerID
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
        public int BinaryVersion
        {
            get => DataPlist["binaryVersion"];
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

        public GameManager() : base(GameDataType.GameManager)
        {
        }

        public GameManager(string fileName) : base(fileName)
        {
        }

        private GameManager(string fileName, bool preventLoading) : base(fileName, preventLoading)
        {
        }
        
        public static async Task<GameManager> LoadAsync(string fileName = null)
        {
            var local = new GameManager(fileName ?? ResolveFileName(GameDataType.GameManager), preventLoading: true);
            await local.Load();
            return local;
        }
    }
}
