using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parser;
using GeometryDashAPI.Tests.TestObjects;

namespace GeometryDashAPI.Tests
{
    [DisassemblyDiagnoser]
    public class GDParserBenchmark
    {
        private GeometryDashObjectParser parser;
        private string largeRaw;

        [GlobalSetup]
        public void Setup()
        {
            parser = new GeometryDashObjectParser();
            largeRaw = parser.Encode(new LargeObject());
        }
        
        [Benchmark]
        public void Encode()
        {
            parser.Encode(new LargeObject());
        }

        [Benchmark]
        public void Decode()
        {
            parser.Decode<LargeObject>(largeRaw);
        }
    }
}