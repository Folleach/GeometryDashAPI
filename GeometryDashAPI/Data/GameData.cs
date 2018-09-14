using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Parser;
using System;
using System.IO;
using System.Text;

namespace GeometryDashAPI.Data
{
    public class GameData
    {
        public Plist DataPlist { get; protected set; }

        private string GameDataFile;

        public GameData(GameDataType type)
        {
            this.GameDataFile = $@"C:\users\{Environment.UserName}\AppData\Local\GeometryDash\CC{type.ToString()}.dat";
            this.Load();
        }

        public GameData(string fullName)
        {
            this.GameDataFile = fullName;
            this.Load();
        }

        public void Load()
        {
            if (!File.Exists(this.GameDataFile))
                throw new FileNotFoundException();

            //File > Byte > XOR > ToString > Replace > Base64 > Gzip
            byte[] data = File.ReadAllBytes(this.GameDataFile);
            string datazip = Encoding.ASCII.GetString(Crypt.XOR(data, 0xB)).Replace("_", "/").Replace("-", "+").Split('\0')[0];
            string resultPlist = Crypt.GZipDecompress(Convert.FromBase64String(datazip));
#if DEBUG
            Console.WriteLine("Create plist \"in\" and \"out\" file");
            File.WriteAllText("plist_in.txt", Plist.PlistToString(new Plist(Encoding.ASCII.GetBytes(resultPlist))));
            File.WriteAllText("plist_out.txt", resultPlist);
#endif
            this.DataPlist = new Plist(Encoding.ASCII.GetBytes(resultPlist));
        }

        public void Save(string fullName = null)
        {
            //Plist > ToString > GetBytes > Gzip > Base64 > Replace > GetBytes > XOR > File
            byte[] gzipc = Crypt.GZipCompress(Encoding.ASCII.GetBytes(Plist.PlistToString(this.DataPlist)));
            string base64 = Convert.ToBase64String(gzipc).Replace('/', '_').Replace('+', '-');
            if (fullName == null)
                File.WriteAllBytes(this.GameDataFile,  Crypt.XOR(Encoding.ASCII.GetBytes(base64), 0xB));
            else
                File.WriteAllBytes(fullName, Crypt.XOR(Encoding.ASCII.GetBytes(base64), 0xB));
        }
    }
}
