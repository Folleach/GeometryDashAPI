using System.IO;
using System.Net;
using BenchmarkDotNet.Attributes;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Parsers;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Responses;

namespace GeometryDashAPI.Benchmarks.Benchmarks
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public class LevelLoadBenchmark
    {
        private string levelRaw;
        private IGameParser oldParser;
        private IGameParser newParser;

        [GlobalSetup]
        public void SetUp()
        {
            levelRaw = File.ReadAllText(@"C:\Users\Andrey\Documents\GitHub\GeometryDashAPI\cromulent.txt");
            GeometryDashApi.parser = oldParser;
            LevelOld.parser = oldParser = new ObjectParserOld();
            Level.parser = newParser = new ObjectParser();
        }

        [Benchmark]
        public LevelOld LoadOld()
        {
            GeometryDashApi.parser = new ObjectParserOld();
            var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, levelRaw);
            GeometryDashApi.parser = oldParser;
            return new LevelOld(response.GetResultOrDefault().Level.LevelString, true);
        }
        
        [Benchmark]
        public Level LoadNew()
        {
            GeometryDashApi.parser = new ObjectParserOld();
            var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, levelRaw);
            GeometryDashApi.parser = newParser;
            return new Level(response.GetResultOrDefault().Level.LevelString, true);
        }
    }
}
