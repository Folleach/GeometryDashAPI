using GeometryDashAPI.Data.Enums;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data
{
    public class GameData
    {
        public Plist DataPlist { get; set; }

        private readonly GameDataType? type;

        public GameData()
        {
        }

        protected GameData(GameDataType type)
        {
            this.type = type;
        }

        public virtual async Task LoadAsync(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"file does not exists: '{fileName}'");

            // File > Bytes > XOR > ToString > Replace > Base64 > Gzip
#if NETSTANDARD2_1
            var data = await File.ReadAllBytesAsync(fileName);
#else
            using var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            var data = new byte[file.Length];
            var read = await file.ReadAsync(data, 0, data.Length);
#endif
            var dataZip = Encoding.ASCII.GetString(Crypt.XOR(data, 0xB)).Split('\0')[0];
            var resultPlist = Crypt.GZipDecompress(GameConvert.FromBase64(dataZip));

            DataPlist = new Plist(Encoding.ASCII.GetBytes(resultPlist));
        }

        /// <summary>
        /// Saves class data to a file as a game save<br/><br/>
        /// Before saving, make sure that you have closed the game. Otherwise, after closing, the game will overwrite the file<br/>
        /// </summary>
        /// <param name="fullName">File to write the data.<br />
        /// use <b>null</b> value for default resolving
        /// </param>
        public void Save(string? fullName = null)
        {
            using var memory = new MemoryStream();
            DataPlist.SaveToStream(memory);
            File.WriteAllBytes(fullName ?? ResolveFileName(type), GetFileContent(memory));
        }

        /// <summary>
        /// Saves class data to a file as a game save<br/><br/>
        /// Before saving, make sure that you have closed the game. Otherwise, after closing, the game will overwrite the file<br/>
        /// </summary>
        /// <param name="fileName">File to write the data.<br />
        /// use <b>null</b> value for default resolving
        /// </param>
        public async Task SaveAsync(string? fileName = null)
        {
            using var memory = new MemoryStream();
            await DataPlist.SaveToStreamAsync(memory);
#if NETSTANDARD2_1
            await File.WriteAllBytesAsync(fileName ?? ResolveFileName(type), GetFileContent(memory));
#else
            using var file = new FileStream(fileName ?? ResolveFileName(type), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            var data = GetFileContent(memory);
            await file.WriteAsync(data, 0, data.Length);
#endif
        }

        public static string ResolveFileName(GameDataType? type)
        {
            if (type == null)
                throw new InvalidOperationException("can't resolve the directory with the saves for undefined file type. Use certain file name");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return $@"{Environment.GetEnvironmentVariable("LocalAppData")}\GeometryDash\CC{type}.dat";
            throw new InvalidOperationException($"can't resolve the directory with the saves on your operating system: '{RuntimeInformation.OSDescription}'. Use certain file name");
        }

        private static byte[] GetFileContent(MemoryStream memory)
        {
            var base64 = GameConvert.ToBase64(Crypt.GZipCompress(memory.ToArray()));
            return Crypt.XOR(Encoding.ASCII.GetBytes(base64), 0xB);
        }
    }
}
