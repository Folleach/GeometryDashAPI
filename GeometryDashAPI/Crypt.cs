using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace GeometryDashAPI
{
    public class Crypt
    {
        // The MacOS save file is not encoded like the Windows one - instead, it uses AES ECB encryption.
        // Huge thanks to: https://github.com/qimiko/gd-save-tools/blob/b5176eb2c805ca65da3e51701409b72b90bdd497/assets/js/savefile.mjs#L43
        private static byte[] MAC_SAVE_KEY =
        [
            0x69, 0x70, 0x75, 0x39, 0x54, 0x55, 0x76, 0x35,
            0x34, 0x79, 0x76, 0x5D, 0x69, 0x73, 0x46, 0x4D,
            0x68, 0x35, 0x40, 0x3B, 0x74, 0x2E, 0x35, 0x77,
            0x33, 0x34, 0x45, 0x32, 0x52, 0x79, 0x40, 0x7B
        ];
        
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

        public static Stream? GZipDecompress(byte[] data)
        {
            if (data == null || data.Length <= 0)
                return null;

            var stream = new MemoryStream(data);
            return new GZipStream(stream, CompressionMode.Decompress);
        }
        
        public static Stream? ZLibDecompress(byte[] data)
        {
            if (data == null || data.Length <= 0)
                return null;

            var stream = new MemoryStream(data);
            return new InflaterInputStream(stream);
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
        
        public static byte[] SavingSaveAsMacOS(byte[] data)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = MAC_SAVE_KEY;
                aesAlg.Mode = CipherMode.ECB; 

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }
        
        public static string LoadSaveAsMacOS(byte[] data)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = MAC_SAVE_KEY;
                aesAlg.Mode = CipherMode.ECB; 

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(data))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
