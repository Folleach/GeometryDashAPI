using System.IO;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Data;
using TestObjects;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class DataLoadBenchmark
{
    private static readonly int LineLength = 1024;
    private static readonly int LineRepeatTimes = 10;
    private static readonly string FileName = "sample";

    [GlobalSetup]
    public void SetUp()
    {
        var sample = GameManagerObjects.CreateSample(LineLength, LineRepeatTimes);
        sample.Save(FileName);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        File.Delete(FileName);
    }

    [Benchmark]
    public GameManager CreateLargeObjectFromCode()
    {
        return GameManagerObjects.CreateSample(LineLength, LineRepeatTimes);
    }

    [Benchmark]
    public GameManager DecodeLargeObjectFromFile()
    {
        return GameManager.LoadFile(FileName);
    }
}
