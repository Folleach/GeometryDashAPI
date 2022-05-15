using System.IO;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Levels;

namespace GeometryDashAPI.Tests
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public class LevelLoadBenchmark
    {
        private string rawLevel;
        
        [GlobalSetup]
        public void SetUp()
        {
            rawLevel = File.ReadAllText("BenchmarkSources/SampleLevel.txt");
        }

        [Benchmark]
        public void Load()
        {
            new Level(rawLevel, false);
        }
    }
}