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
        // This is xored gzip magick bytes: 'C?'
        // see more https://en.wikipedia.org/wiki/Gzip
        private static readonly byte[] XorDatFileMagickBytes = [ 0x43, 0x3f ];

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
            _ = await file.ReadAsync(data, 0, data.Length);

            if (data.AsSpan().Slice(0, XorDatFileMagickBytes.Length).IndexOf(XorDatFileMagickBytes) != 0)
            {
                // mac files
                var decryptedData = Crypt.LoadSaveAsMacOS(data);
                DataPlist = new Plist(Encoding.ASCII.GetBytes(decryptedData));
                return;
            }

            // windows files
            var xor = Crypt.XOR(data, 0xB);
            var index = xor.AsSpan().IndexOf((byte)0);
            var gZipDecompress =
                Crypt.GZipDecompress(
                    GameConvert.FromBase64(Encoding.ASCII.GetString(xor, 0, index >= 0 ? index : xor.Length)));
          
            if (gZipDecompress is null)
                throw new InvalidOperationException("Data was empty");

            DataPlist = new Plist(gZipDecompress);
        }

        /// <summary>
        /// Saves class data to a file as a game save<br/><br/>
        /// Before saving, make sure that you have closed the game. Otherwise, after closing, the game will overwrite the file<br/>
        /// </summary>
        /// <param name="fullName">File to write the data.<br />
        /// use <b>null</b> value for default resolving
        /// </param>
        /// <param name="format">
        /// Specify if you want to save the file in a format specific to another operating system.<br />
        /// Leave <b>null</b> to save the file for the current operating system
        /// </param>
        public void Save(string? fullName = null, DatFileFormat? format = null)
        {
            using var memory = new MemoryStream();
            DataPlist.SaveToStream(memory);
            File.WriteAllBytes(fullName ?? ResolveFileName(type), GetFileContent(memory, format ?? ResolveFileFormat()));
        }

        /// <summary>
        /// Saves class data to a file as a game save<br/><br/>
        /// Before saving, make sure that you have closed the game. Otherwise, after closing, the game will overwrite the file<br/>
        /// </summary>
        /// <param name="fileName">File to write the data.<br />
        /// use <b>null</b> value for default resolving
        /// </param>
        /// <param name="format">
        /// Specify if you want to save the file in a format specific to another operating system.<br />
        /// Leave <b>null</b> to save the file for the current operating system
        /// </param>
        public async Task SaveAsync(string? fileName = null, DatFileFormat? format = null)
        {
            using var memory = new MemoryStream();
            await DataPlist.SaveToStreamAsync(memory);
#if NETSTANDARD2_1
            await File.WriteAllBytesAsync(fileName ?? ResolveFileName(type), GetFileContent(memory, format ?? ResolveFileFormat()));
#else
            using var file = new FileStream(fileName ?? ResolveFileName(type), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 4096, useAsync: true);
            var data = GetFileContent(memory, format ?? ResolveFileFormat());
            await file.WriteAsync(data, 0, data.Length);
#endif
        }

        public static string ResolveFileName(GameDataType? type)
        {
            if (type == null)
                throw new InvalidOperationException("can't resolve the directory with the saves for undefined file type. Use certain file name");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return $@"{Environment.GetEnvironmentVariable("LocalAppData")}\GeometryDash\CC{type}.dat";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return $"/Users/{Environment.GetEnvironmentVariable("USER")}/Library/Application Support/GeometryDash/CC{type}.dat";
            throw new InvalidOperationException($"can't resolve the directory with the saves on your operating system: '{RuntimeInformation.OSDescription}'. Use certain file name");
        }

        public static DatFileFormat ResolveFileFormat()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return DatFileFormat.Mac;
            return DatFileFormat.Windows;
        }

        private static byte[] GetFileContent(MemoryStream memory, DatFileFormat format)
        {
            if (format == DatFileFormat.Mac)
                return Crypt.SavingSaveAsMacOS(memory.ToArray());

            var base64 = GameConvert.ToBase64(Crypt.GZipCompress(memory.ToArray()));
            return Crypt.XOR(Encoding.ASCII.GetBytes(base64), 0xB);
        }

        private static bool StartsWith(Stream stream, ReadOnlySpan<byte> prefix)
        {
            if (!stream.CanSeek)
                throw new ArgumentException($"{nameof(stream)} is not seekable. This can lead to bugs.");
            if (stream.Length < prefix.Length)
                return false;
            var position = stream.Position;
            var buffer = new byte[prefix.Length];
            var read = 0;
            while (read != buffer.Length)
                read += stream.Read(buffer, read, buffer.Length - read);
            stream.Seek(position, SeekOrigin.Begin);
            return buffer.AsSpan().IndexOf(prefix) == 0;
        }
    }
}
