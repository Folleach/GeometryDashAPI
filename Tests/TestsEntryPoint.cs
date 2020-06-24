using GeometryDashAPI.Levels.GameObjects;
using System;
using System.Reflection;
using Tests.Documentation;

namespace Tests
{
    public class TestsEntryPoint
    {
        public static int Main(string[] args)
        {
            new BlocksDocumentation().WriteAllSupportedBlockTo(Assembly.GetAssembly(typeof(IBlock)), Console.Out);
            return 0;
        }
    }
}
