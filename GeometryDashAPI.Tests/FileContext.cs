using System;
using System.IO;

namespace GeometryDashAPI.Tests;

public class FileContext : IDisposable
{
    private readonly bool removeAfterDispose;
    private const string DirectoryName = "test_files";
    public string Name { get; }

    public FileContext(string name, bool removeAfterDispose = true)
    {
        this.removeAfterDispose = removeAfterDispose;
        Name = Path.Combine(DirectoryName, name);
        if (!Directory.Exists(DirectoryName))
            Directory.CreateDirectory(DirectoryName);
        if (File.Exists(Name))
            throw new InvalidOperationException("temp file already exists, try to re run tests");
        using var file = File.Create(Name);
    }

    public void Dispose()
    {
        if (removeAfterDispose)
            File.Delete(Name);
    }

    public static FileContext Create(bool removeAfterDispose = true)
    {
        return new FileContext(Guid.NewGuid().ToString("N"), removeAfterDispose);
    }
}
