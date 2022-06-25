using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;
using TestObjects;

namespace GeometryDashAPI.Tests.Benchmarks
{
    [DisassemblyDiagnoser]
    public class GdParserBenchmark
    {
        private string largeRaw;

        [GlobalSetup]
        public void Setup()
        {
            largeRaw = ObjectParserOld.Encode(new LargeObject());
        }
        
        [Benchmark]
        public void Encode()
        {
            ObjectParserOld.Encode(new LargeObject());
        }

        [Benchmark]
        public void Decode()
        {
            ObjectParserOld.Decode<LargeObject>(largeRaw);
        }
    }
}