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
            largeRaw = ObjectParser.Encode(new LargeObject());
        }
        
        [Benchmark]
        public void Encode()
        {
            ObjectParser.Encode(new LargeObject());
        }

        [Benchmark]
        public void Decode()
        {
            ObjectParser.Decode<LargeObject>(largeRaw);
        }
    }
}