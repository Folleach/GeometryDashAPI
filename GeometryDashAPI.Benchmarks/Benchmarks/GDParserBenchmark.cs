using System;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Serialization;
using TestObjects;

namespace GeometryDashAPI.Benchmarks.Benchmarks
{
    [DisassemblyDiagnoser]
    public class GdParserBenchmark
    {
        private string largeRaw;
        private static ObjectSerializer serializer;

        [Params(typeof(ObjectSerializer))]
        public Type ParserParam;

        [GlobalSetup]
        public void Setup()
        {
            throw new NotImplementedException();
            // largeRaw = parser.Encode(new LargeObject());
            serializer = (ObjectSerializer)Activator.CreateInstance(ParserParam);
            GeometryDashApi.Serializer = serializer;
        }

        [Benchmark]
        public void Encode()
        {
            throw new NotImplementedException();
            // parser.Encode(new LargeObject());
        }

        [Benchmark]
        public void Decode()
        {
            serializer.Decode<LargeObject>(largeRaw);
        }
    }
}
