using GeometryDashAPI.Data.Enums;
using System;
using System.Buffers;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI;
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
            await using var xorStream = new XorStream(file, 0xB);
            await using var base64Stream = new AsciiBase64Stream(xorStream);
            await using var gzip = new GZipStream(base64Stream, CompressionMode.Decompress);
#else
            using var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            using var xorStream = new XorStream(file, 0xB);
            using var base64Stream = new AsciiBase64Stream(xorStream);
            using var gzip = new GZipStream(base64Stream, CompressionMode.Decompress);
#endif

            DataPlist = new Plist(gzip);
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
            WriteContent(fullName ?? ResolveFileName(type), memory).GetAwaiter().GetResult();
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
            await WriteContent(fileName ?? ResolveFileName(type), memory);
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

        private static async Task WriteContent(string file, MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
#if NETSTANDARD2_1
            await using var destination = File.OpenWrite(file);
            await using var xorStream = new XorStream(destination, 0xB);
            await using var base64Stream = new AsciiBase64Stream(xorStream);
            var gzip = new GZipStream(base64Stream, CompressionMode.Compress);
#else
            using var destination = File.OpenWrite(file);
            using var xorStream = new XorStream(destination, 0xB);
            using var base64Stream = new AsciiBase64Stream(xorStream);
            var gzip = new GZipStream(base64Stream, CompressionMode.Compress);
#endif

            using (gzip)
            {
                using (stream)
                    await stream.CopyToAsync(gzip);
            }

            await base64Stream.FlushAsync();
            await xorStream.FlushAsync();
            await destination.FlushAsync();
        }
    }
}

public class XorStream : Stream
{
    private readonly Stream inner;
    private readonly byte xor;

    public XorStream(Stream inner, byte xor)
    {
        this.inner = inner;
        this.xor = xor;
    }

    public override void Flush()
    {
        inner.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var read = inner.Read(buffer, offset, count);
        Crypt.InlineXor(buffer.AsSpan(offset, read), xor);
        return read;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return inner.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        inner.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        var length = count - offset;
        var data = ArrayPool<byte>.Shared.Rent(count - offset);
        buffer.AsSpan(offset, count).CopyTo(data);
        Crypt.InlineXor(data, xor);
        inner.Write(data, 0, length);
    }

    public override bool CanRead => inner.CanRead;
    public override bool CanSeek => inner.CanSeek;
    public override bool CanWrite => inner.CanWrite;
    public override long Length => inner.Length;
    public override long Position
    {
        get => inner.Position;
        set => inner.Position = value;
    }
}

public class AsciiBase64Stream : Stream
{
    private readonly Stream inner;
    private byte[] writeTail = new byte[4];
    private int writeTailSize = 0;

    public AsciiBase64Stream(Stream inner)
    {
        this.inner = inner;
    }

    public override void Flush()
    {
        if (writeTailSize > 0)
            inner.Write(writeTail, 0, writeTailSize);
        inner.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
#if NETSTANDARD2_1
        var overflow = count % 4;
        count -= overflow;
        var transform = ArrayPool<byte>.Shared.Rent(count);
        var read = inner.Read(transform);
        overflow = read % 4;
        read -= overflow;
        var str = Encoding.ASCII.GetString(transform.AsSpan(0, read));
        var result = GameConvert.FromBase64(str);
        result.CopyTo(buffer.AsSpan(offset));
        ArrayPool<byte>.Shared.Return(transform);
        return result.Length;
#endif
        return 0;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return inner.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        inner.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
#if NETSTANDARD2_1
        var data = writeTailSize > 0
            ? ArrayPool<byte>.Shared.Rent(count - offset + writeTailSize)
            : ArrayPool<byte>.Shared.Rent(count - offset);

        var dataLength = writeTailSize > 0 ? count - offset + writeTailSize : count - offset;

        if (writeTailSize > 0)
            writeTail.CopyTo(data, 0);
        buffer.AsSpan(offset, count).CopyTo(data.AsSpan(writeTailSize, dataLength - writeTailSize));

        writeTailSize = dataLength % 3;
        if (writeTailSize != 0)
            data.AsSpan(dataLength - writeTailSize, writeTailSize).CopyTo(writeTail);
        var base64 = GameConvert.ToBase64(data.AsSpan(0, dataLength - writeTailSize));
        var transform = ArrayPool<byte>.Shared.Rent(base64.Length);
        var bytes = Encoding.ASCII.GetBytes(base64, transform);
        inner.Write(transform, 0, bytes);
        ArrayPool<byte>.Shared.Return(transform);
        ArrayPool<byte>.Shared.Return(data);
#endif
    }

    public override bool CanRead => inner.CanRead;
    public override bool CanSeek => inner.CanSeek;
    public override bool CanWrite => inner.CanWrite;
    public override long Length => inner.Length;
    public override long Position
    {
        get => inner.Position;
        set => inner.Position = value;
    }
}
