using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[DisassemblyDiagnoser]
[MemoryDiagnoser]
public class ObjectParserBenchmark
{
    private static IGameSerializer serializer = new ObjectSerializer();

    [Benchmark]
    public int[] GetArray()
    {
        return serializer.GetArray("33,1,33,2,33,3,33,4,33,5,33,6,33,7,33,8,33,9", ",", Parsers.GetOrDefault_Int32__);
    }
}
