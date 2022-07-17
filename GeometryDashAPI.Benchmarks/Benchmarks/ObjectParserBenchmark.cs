using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Benchmarks.Benchmarks;

[DisassemblyDiagnoser]
[MemoryDiagnoser]
public class ObjectParserBenchmark
{
    private static IGameParser parser = new ObjectParser();

    [Benchmark]
    public int[] GetArray()
    {
        return parser.GetArray("33,1,33,2,33,3,33,4,33,5,33,6,33,7,33,8,33,9", ",", Parsers.Parsers.GetOrDefault_Int32__);
    }
}
