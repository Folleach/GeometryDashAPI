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

#if NETSTANDARD2_1
            await using var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
#else
            using var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
#endif
            var data = new byte[file.Length];
            await file.ReadAsync(data, 0, data.Length);

            var xor = Crypt.XOR(data, 0xB);
            var index = xor.GetIndexOfNullByte();
            var gZipDecompress = Crypt.GZipDecompress(GameConvert.FromBase64(Encoding.ASCII.GetString(xor, 0, index)));

            DataPlist = new Plist(Encoding.ASCII.GetBytes(gZipDecompress));
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
            using var file = new FileStream(fileName ?? ResolveFileName(type), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 4096, useAsync: true);
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
