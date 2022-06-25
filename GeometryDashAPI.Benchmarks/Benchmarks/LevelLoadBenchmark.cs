using System;
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
        private IGameParser parser;
        
        [Params(typeof(ObjectParserOld), typeof(ObjectParser))]
        public Type ParserParam;
        
        [GlobalSetup]
        public void SetUp()
        {
            levelRaw = File.ReadAllText(@"C:\Users\Andrey\Documents\GitHub\GeometryDashAPI\cromulent.txt");
            parser = (IGameParser)Activator.CreateInstance(ParserParam);
            GeometryDashApi.parser = parser;
            Level.parser = parser;
        }

        [Benchmark]
        public Level Load()
        {
            var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, levelRaw);
            return new Level(response.GetResultOrDefault().Level.LevelString, true);
        }
    }
}
