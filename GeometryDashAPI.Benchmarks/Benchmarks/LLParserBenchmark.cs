using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Tests
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class LLParserBenchmark
    {
        private string value;
        private string tripleValue;
        private string separator = ".";

        [Params(10000)]
        public int CountOfValues;
        
        [GlobalSetup]
        public void SetUp()
        {
            value = string.Join(separator, Enumerable.Range(0, CountOfValues).Select(x => $"x"));
            tripleValue = string.Join(separator, Enumerable.Range(0, CountOfValues).Select(x => $"xxx"));
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
            var parser = new LLParserSpan(separator.AsSpan(), value.AsSpan());
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }
        
        [Benchmark]
        public void TripleNext_Old()
        {
            var parser = new LLParser(separator, tripleValue);
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }
        
        [Benchmark]
        public void TripleNext_New()
        {
            var parser = new LLParserSpan(separator.AsSpan(), tripleValue.AsSpan());
            while (true)
            {
                var token = parser.Next();
                if (token == null)
                    break;
            }
        }
    }
}