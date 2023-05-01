using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace GeometryDashAPI.Tests.Integration.Levels;

[TestFixture(Explicit = true, Reason = "use a lot of levels to compare compability with Geometry Dash")]
[Category("Integration")]
[Parallelizable(ParallelScope.All)]
public class LevelResponseFromStreamTest : LevelResponseTestBase
{
    private const string ProcessDataPathEnv = "GDAPI_TESTS_CONTENTS";
    private const string ProcessLevelcontentFilename = "levels";

    public static IEnumerable<TestCaseData> Source
        = File.ReadLines(
            Path.Combine(
                GetContentPath(),
                ProcessLevelcontentFilename)
            )
            .Select(x => new TestCaseData(x)
            {
                TestName = x.Substring(0, 30)
            });

    private static string GetContentPath()
        => Environment.GetEnvironmentVariable(ProcessDataPathEnv)
           ?? Environment.GetEnvironmentVariable(ProcessDataPathEnv, EnvironmentVariableTarget.User)
           ?? throw new InvalidOperationException($"'{ProcessDataPathEnv}' is not set");

    [TestCaseSource(nameof(Source))]
    public void LoadAndSaveLevel(string responseBody)
    {
        Test(responseBody);
    }
}
