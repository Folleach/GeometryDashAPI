using System.IO;
using System.IO.Compression;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace GeometryDashAPI
{
    public class Crypt
    {
        public static byte[] XOR(byte[] data, int key)
        {
            var result = new byte[data.Length];
            for (var i = 0; i < data.Length; i++)
                result[i] = (byte)(data[i] ^ key);
            return result;
        }

        public static string XOR(string text, string key)
        {
            var result = new StringBuilder();
            for (var c = 0; c < text.Length; c++)
                result.Append((char)(text[c] ^ key[c % key.Length]));
            return result.ToString();
        }

        public static string GZipDecompress(byte[] data)
        {
            if (data == null || data.Length <= 0)
                return string.Empty;
            using var stream = new MemoryStream(data);
            using var zip = new GZipStream(stream, CompressionMode.Decompress);
            using var reader = new StreamReader(zip);
            return reader.ReadToEnd();
        }
        
        public static string ZLibDecompress(byte[] data)
        {
            if (data == null || data.Length <= 0)
                return string.Empty;
            using var stream = new MemoryStream(data);
            using var zip = new InflaterInputStream(stream);
            using var reader = new StreamReader(zip);
            return reader.ReadToEnd();
        }

        public static byte[] GZipCompress(byte[] data)
        {
            using var memory = new MemoryStream();
            using (var destination = new GZipStream(memory, CompressionMode.Compress))
            {
                using var memoryStream2 = new MemoryStream(data);
                memoryStream2.CopyTo(destination);
            }
            return memory.ToArray();
        }
    }
}
