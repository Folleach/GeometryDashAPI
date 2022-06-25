using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Tests
{
    [DisassemblyDiagnoser]
    public class LLParserBenchmark
    {
        private string value;
        private string separator = ".";

        [Params(100, 1000, 10000)]
        public int N;
        
        [GlobalSetup]
        public void SetUp()
        {
            value = string.Join(separator, Enumerable.Range(0, N).Select(x => $"x"));
        }
        
        [Benchmark]
        public void Next()
        {
            var parser = new LLParser(separator, value);
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }
    }
}