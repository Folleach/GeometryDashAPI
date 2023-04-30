using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using FluentAssertions;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Responses;
using NUnit.Framework;

namespace GeometryDashAPI.Tests.Integration;

[TestFixture(Explicit = true, Reason = "use a lot of levels to compare compability with Geometry Dash")]
[Category("Integration")]
public class LevelResponseTest
{
    private const string PROCESS_DATA_PATH_ENV = "GDAPI_TESTS_CONTENTS";
    private const string PROCESS_LEVELCONTENT_FILENAME = "levels";
    private const string FAILED_TEST_DIRECTORY_PREFIX = "failed";

    private static readonly int? TrimFailedLines = 100;

    public static IEnumerable<TestCaseData> Source
        = File.ReadLines(
            Path.Combine(
                Environment.GetEnvironmentVariable(PROCESS_DATA_PATH_ENV)
                    ?? Environment.GetEnvironmentVariable(PROCESS_DATA_PATH_ENV, EnvironmentVariableTarget.User)
                    ?? throw new InvalidOperationException($"'{PROCESS_DATA_PATH_ENV}' is not set"),
                PROCESS_LEVELCONTENT_FILENAME)
            )
            .Select(x => new TestCaseData(x)
            {
                TestName = x.Substring(0, 30)
            });

    [TestCaseSource(nameof(Source))]
    public void LoadAndSaveLevel(string responseBody)
    {
        var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, responseBody);
        var result = response.GetResultOrDefault();
        result.WithoutLoaded.Should().HaveCount(0,
            $"'{nameof(result.WithoutLoaded)}' in _response body_ contains any elements. It's wrong for full compability");

        result.Level.WithoutLoaded.Should().HaveCount(0,
            $"'{nameof(result.WithoutLoaded)}' in _level preview_ contains any elements. It's wrong for full compability");

        var decompressedLevel = Level.Decompress(result.Level.LevelString);
        var level = new Level(decompressedLevel, compressed: false);
        var actualLevel = level.SaveAsString(compress: false);

        // self compare
        // var level2 = new Level(actualLevel, compressed: false);
        // level2.Should().BeEquivalentTo(level);

        Compare(decompressedLevel, actualLevel);
        // SaveFailed(decompressedLevel, actualLevel);
    }

    // fluent assertions objects compare too slow
    private static void Compare(string decompressedLevel, string actualLevel)
    {
        var expectedBlocks = GetBlocksAsDictionary(decompressedLevel).ToArray();
        var actualBlocks = GetBlocksAsDictionary(actualLevel).ToArray();

        actualBlocks.Should().HaveCount(expectedBlocks.Length);

        for (var i = 0; i < expectedBlocks.Length; i++)
        {
            var expected = expectedBlocks[i];
            var actual = actualBlocks[i];

            // todo: perhaps this is a bug. Ideally, to fix it need to support all types of blocks
            actual.Remove("25");
            expected.Remove("25");
            actual.Remove("24");
            expected.Remove("24");

            actual.Remove("21");
            expected.Remove("21");

            foreach (var first in expected)
            {
                if (!actual.TryGetValue(first.Key, out var second))
                    Assert.Fail($"{first.Key} is disappeared. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
                if (!first.Value.Equals(second, StringComparison.OrdinalIgnoreCase))
                    Assert.Fail($"{first.Key} value isn't same. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
            }
            foreach (var first in actual)
            {
                if (!expected.TryGetValue(first.Key, out var second))
                    Assert.Fail($"{first.Key} is appeared. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
                if (!first.Value.Equals(second, StringComparison.OrdinalIgnoreCase))
                    Assert.Fail($"{first.Key} value isn't same. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
            }
        }
    }

    private static string CreateExpectedAndActual(Dictionary<string, string> expected, Dictionary<string, string> actual)
    {
        return @$"
expected: '{string.Join(",", expected.Select(x => new[] {x.Key, x.Value}).SelectMany(x => x))}'
actual: '{string.Join(",", actual.Select(x => new[] {x.Key, x.Value}).SelectMany(x => x))}'
";
    }

    private static IEnumerable<Dictionary<string, string>> GetBlocksAsDictionary(string value)
        => value
            .Split(';')
            .Skip(1)
            .Select(x => x.Split(','))
            .Where(x => x.Length > 1)
            .Select(x => x.Pairs().ToDictionary(p => p.Key, p => p.Value));

    private static void SaveFailed(string expected, string actual)
    {
        var failedTime = DateTime.UtcNow;
        var directory = Path.Combine(FAILED_TEST_DIRECTORY_PREFIX);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        var fileExpected = Path.Combine(directory, $"{failedTime:yyyy-MM-ddThh:mm:ss}_{TestContext.CurrentContext.Test.Name}_expected");
        var fileResult = Path.Combine(directory, $"{failedTime:yyyy-MM-ddThh:mm:ss}_{TestContext.CurrentContext.Test.Name}_result");
        
        File.WriteAllLines(fileExpected, expected.Split(';').Take(TrimFailedLines ?? int.MaxValue));
        File.WriteAllLines(fileResult, actual.Split(';').Take(TrimFailedLines ?? int.MaxValue));
        
        TestContext.Out.WriteLine($"expected and actual values saved at '{Path.Combine(Environment.CurrentDirectory, directory)}'");
    }
}
