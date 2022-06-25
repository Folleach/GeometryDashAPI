using System;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;
using TestObjects;

namespace GeometryDashAPI.Benchmarks.Benchmarks
{
    [DisassemblyDiagnoser]
    public class GdParserBenchmark
    {
        private string largeRaw;
        private static IGameParser parser;

        [Params(typeof(ObjectParserOld), typeof(ObjectParser))]
        public Type ParserParam;

        [GlobalSetup]
        public void Setup()
        {
            largeRaw = parser.Encode(new LargeObject());
            parser = (IGameParser)Activator.CreateInstance(ParserParam);
            GeometryDashApi.parser = parser;
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
