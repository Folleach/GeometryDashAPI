using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class XorBenchmark
{
    private static Random random = new(123);
    private byte[] array = Enumerable.Range(0, 1000).Select(x => (byte)random.Next(byte.MaxValue)).ToArray();

    [Benchmark]
    public void SimdXor()
    {
        Crypt.InlineXor(array, 11);
    }

    [Benchmark]
    public void NaiveXor()
    {
        Crypt.NaiveXor(array, 11);
    }

    [Benchmark]
    public byte[] OldXor()
    {
        return Crypt.XOR(array, 11);
    }
}
