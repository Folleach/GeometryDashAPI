using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Benchmarks.Benchmarks
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public class LLParserBenchmark
    {
        private string input;
        private string separator = ".";

        [Params(10000)]
        public int ItemsCount;

        [Params(1, 5, 20)]
        public int ValueLength;

        [GlobalSetup]
        public void SetUp()
        {
            input = string.Join(separator, Enumerable.Range(0, ItemsCount).Select(x => new string('x', ValueLength)));
        }

        [Benchmark]
        public void LLParserSpan_EnumerateValues()
        {
            var parser = new LLParserSpan(separator, input);
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }

        [Benchmark]
        public void Split_EnumerateValues()
        {
            foreach (var token in input.Split(separator))
            {
            }
        }
    }
}
