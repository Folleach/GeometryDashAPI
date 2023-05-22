using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Memory;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data
{
    public class GameData
    {
        public Plist DataPlist { get; protected set; }

        private readonly string gameDataFile;

        public GameData(GameDataType type)
        {
            gameDataFile = $@"{Environment.GetEnvironmentVariable("LocalAppData")}\GeometryDash\CC{type}.dat";
            Load().GetAwaiter().GetResult();
        }

        public GameData(string fullName)
        {
            gameDataFile = fullName;
            Load().GetAwaiter().GetResult();
        }

        protected GameData(string fullName, bool preventLoading)
        {
            gameDataFile = fullName;
            if (preventLoading)
                return;
            Load().GetAwaiter().GetResult();
        }

        public async Task Load()
        {
            if (!File.Exists(gameDataFile))
                throw new FileNotFoundException();

            // File > Bytes > XOR > ToString > Replace > Base64 > Gzip
            var data = await File.ReadAllBytesAsync(gameDataFile);
            var dataZip = Encoding.ASCII.GetString(Crypt.XOR(data, 0xB)).Split('\0')[0];
            var resultPlist = Crypt.GZipDecompress(GameConvert.FromBase64(dataZip));

            DataPlist = new Plist(Encoding.ASCII.GetBytes(resultPlist));
            GC.Collect();
        }

        public virtual bool TrySave(bool checkRunningGame, string fullName = null)
        {
            if (checkRunningGame && GameProcess.GameCount() > 0)
                return false;

            return this.TrySave(fullName);
        }

        public virtual bool TrySave(string fullName = null)
        {
            //Plist > ToString > GetBytes > Gzip > Base64 > Replace > GetBytes > XOR > File
            byte[] gzipc = Crypt.GZipCompress(Encoding.ASCII.GetBytes(Plist.PlistToString(DataPlist)));
            string base64 = GameConvert.ToBase64(gzipc);

            File.WriteAllBytes(fullName ?? this.GameDataFile, Crypt.XOR(Encoding.ASCII.GetBytes(base64), 0xB));
            return true;
        }
    }
}
