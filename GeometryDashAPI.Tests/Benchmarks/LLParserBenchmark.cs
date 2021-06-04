using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Tests
{
    [DisassemblyDiagnoser]
    public class LLParserBenchmark
    {
        private LLParser parser;
        private string separator = ".";

        [Params(1000, 10000)]
        public int N;
        
        [GlobalSetup]
        public void SetUp()
        {
            parser = new LLParser(separator, string.Join(separator, Enumerable.Range(0, N).Select(x => "x")));
        }
        
        [Benchmark]
        public void Next()
        {
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }
    }
}