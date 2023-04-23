using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Serialization;
using TestObjects;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[DisassemblyDiagnoser(maxDepth: 16)]
public class DisassemblyBenchmark
{
    [Benchmark]
    public ObjectSample CreateByDescriptor()
    {
        var descriptor = new TypeDescriptor<ObjectSample>();
        return descriptor.Create("33:2");
    }
}