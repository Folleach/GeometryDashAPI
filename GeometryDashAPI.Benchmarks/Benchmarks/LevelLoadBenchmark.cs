using System.IO;
using System.Net;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Serialization;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Responses;

namespace GeometryDashAPI.Benchmarks.Benchmarks
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
            Level.Serializer = new ObjectSerializer();
        }

        [Benchmark]
        public Level LoadNew()
        {
            var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, levelRaw);
            return new Level(response.GetResultOrDefault().Level.LevelString, true);
        }
    }
}
