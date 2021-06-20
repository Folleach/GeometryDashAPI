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
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
                result[i] = (byte)(data[i] ^ key);
            return result;
        }

        public static string XOR(string text, string key)
        {
            var result = new StringBuilder();
            for (int c = 0; c < text.Length; c++)
                result.Append((char)(text[c] ^ key[c % key.Length]));
            return result.ToString();
        }

        public static string GZipDecompress(byte[] data)
        {
            var resultString = string.Empty;
            if (data != null && data.Length > 0)
            {
                using (var stream = new MemoryStream(data))
                using (var zip = new GZipStream(stream, CompressionMode.Decompress))
                using (var reader = new StreamReader(zip))
                    resultString = reader.ReadToEnd();
            }
            return resultString;
        }
        
        public static string ZlipDecompress(byte[] data)
        {
            var resultString = string.Empty;
            if (data != null && data.Length > 0)
            {
                using (var stream = new MemoryStream(data))
                using (var zip = new InflaterInputStream(stream))
                using (var reader = new StreamReader(zip))
                    resultString = reader.ReadToEnd();
            }
            return resultString;
        }

        public static byte[] GZipCompress(byte[] data)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(outStream, CompressionMode.Compress))
                using (MemoryStream srcStream = new MemoryStream(data))
                    srcStream.CopyTo(gzipStream);
                return outStream.ToArray();
            }
        }
    }
}
