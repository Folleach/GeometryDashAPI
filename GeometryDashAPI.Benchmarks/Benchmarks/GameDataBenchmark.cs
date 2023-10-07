using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class GameDataBenchmark
    {
        private GameManager manager;
        
        [Benchmark]
        public void ParseSplit()
        {
            manager = GameManager.LoadFile(null);
        }
    }
}
