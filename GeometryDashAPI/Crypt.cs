using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Security.Cryptography;

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
            string resultString = string.Empty;
            if (data != null && data.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(data))
                using (GZipStream zip = new GZipStream(stream, CompressionMode.Decompress))
                using (StreamReader reader = new StreamReader(zip))
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

        public static string GenerateCheck(string[] values, string salt, int key) //combine values->add salt->sha1 hash->XOR->Base64 encode-> return. thanks gdpy (https://github.com/nekitdev/gd.py)
        {
            string check = "";
            foreach (string value in values)
                check += value;
            check += salt;
            byte[] sha1Bytes = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(check));
            check = BitConverter.ToString(sha1Bytes).Replace("-", "").ToLower();
            check = XOR(check, key.ToString());
            check = GameConvert.ToBase64(Encoding.ASCII.GetBytes(check));
            return check;
        }

        public static string GenerateSeed2(string levelString)
        {
            int slot = levelString.Length / 50;
            StringBuilder rawResult = new StringBuilder();
            for (int i = 0; i < 50; i++)
                rawResult.Append(levelString[slot * i]);
            rawResult.Append("xI25fpAapCQg");
            string res = GameConvert.ToBase64(Encoding.ASCII.GetBytes(rawResult.ToString()));
            res = Crypt.XOR(res, "41274");
            return res;
        }
    }
}
