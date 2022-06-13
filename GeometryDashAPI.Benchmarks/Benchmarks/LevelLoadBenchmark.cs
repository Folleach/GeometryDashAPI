using System.IO;
using System.Net;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Responses;

namespace GeometryDashAPI.Tests
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public class LevelLoadBenchmark
    {
        private string levelRaw;
        
        [GlobalSetup]
        public void SetUp()
        {
            levelRaw = File.ReadAllText(@"C:\Users\Andrey\Documents\GitHub\GeometryDashAPI\cromulent.txt");
        }

        [Benchmark]
        public void Load()
        {
            var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, levelRaw);
            new Level(response.GetResultOrDefault().Level.LevelString, true);
        }
    }
}
