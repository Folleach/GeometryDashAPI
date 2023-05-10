using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Memory;
using System;
using System.IO;
using System.Text;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data
{
    public class GameData
    {
        public Plist DataPlist { get; protected set; }

        private string GameDataFile;

        public GameData(GameDataType type)
        {
            this.GameDataFile = $@"{Environment.GetEnvironmentVariable("LocalAppData")}\GeometryDash\CC{type}.dat";
            this.Load();
        }

        public GameData(string fullName)
        {
            this.GameDataFile = fullName;
            this.Load();
        }

        public virtual void Load()
        {
            if (!File.Exists(this.GameDataFile))
                throw new FileNotFoundException();

            //File > Byte > XOR > ToString > Replace > Base64 > Gzip
            byte[] data = File.ReadAllBytes(this.GameDataFile);
            string datazip = Encoding.ASCII.GetString(Crypt.XOR(data, 0xB)).Split('\0')[0];
            string resultPlist = Crypt.GZipDecompress(GameConvert.FromBase64(datazip));

            this.DataPlist = new Plist(Encoding.ASCII.GetBytes(resultPlist));
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
            byte[] gzipc = Crypt.GZipCompress(Encoding.ASCII.GetBytes(Plist.PlistToString(this.DataPlist)));
            string base64 = GameConvert.ToBase64(gzipc);

            File.WriteAllBytes(fullName ?? this.GameDataFile, Crypt.XOR(Encoding.ASCII.GetBytes(base64), 0xB));
            return true;
        }

        public virtual bool TrySave() => this.TrySave(GameDataFile);
    }
}
