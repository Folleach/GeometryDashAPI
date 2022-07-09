using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Benchmarks.Benchmarks
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public class LLParserBenchmark
    {
        private string value;
        private string separator = ".";

        [Params(10000)]
        public int ItemsCount;

        [Params(1, 5)]
        public int ValueLength;

        [GlobalSetup]
        public void SetUp()
        {
            value = string.Join(separator, Enumerable.Range(0, ItemsCount).Select(x => new string('x', ValueLength)));
        }

        [Benchmark]
        public void Next_Old()
        {
            var parser = new LLParser(separator, value);
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }

        [Benchmark]
        public void Next_New()
        {
            var parser = new LLParserSpan(separator, value);
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }
    }
}
