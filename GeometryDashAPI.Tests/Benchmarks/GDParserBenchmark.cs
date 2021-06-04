using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parser;
using GeometryDashAPI.Parsers;
using GeometryDashAPI.Tests.TestObjects;

namespace GeometryDashAPI.Tests
{
    [DisassemblyDiagnoser]
    public class GDParserBenchmark
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