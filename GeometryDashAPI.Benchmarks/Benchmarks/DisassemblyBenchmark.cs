using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;
using TestObjects;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[DisassemblyDiagnoser(maxDepth: 16)]
public class DisassemblyBenchmark
{
    [Benchmark]
    public ObjectSample CreateByDescriptor()
    {
        var descriptor = new TypeDescriptor<ObjectSample, int>();
        return descriptor.Create("33:2");
    }
}