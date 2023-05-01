using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using FluentAssertions;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Serialization;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Responses;
using NUnit.Framework;

namespace GeometryDashAPI.Tests.Integration.Levels;

public class LevelResponseTestBase
{
    protected void Test(string responseBody)
    {
        var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, responseBody);
        var result = response.GetResultOrDefault();
        result.WithoutLoaded.Should().HaveCount(0,
            $"'{nameof(result.WithoutLoaded)}' in _response body_ contains any elements. It's wrong for full compability");

        result.Level.WithoutLoaded.Should().HaveCount(0,
            $"'{nameof(result.WithoutLoaded)}' in _level preview_ contains any elements. It's wrong for full compability");

        var decompressedLevel = Level.Decompress(result.Level.LevelString);
        var level = new Level(decompressedLevel, compressed: false);

        level.Options.WithoutLoaded.Should().HaveCount(0);

        var actualLevel = level.SaveAsString(compress: false);

        // self compare
        // var level2 = new Level(actualLevel, compressed: false);
        // level2.Should().BeEquivalentTo(level);

        Compare(decompressedLevel, actualLevel);
        // SaveFailed(decompressedLevel, actualLevel);
    }

    private static readonly Dictionary<string, string> ColorDefaults = new()
    {
        ["11"] = "255",
        ["12"] = "255",
        ["13"] = "255",
        ["18"] = "0"
    };

    private static readonly Dictionary<string, string> BlockDefaults = new()
    {
        ["85"] = "2",
        ["96"] = "1",
        ["22"] = "1"
    };

    // fluent assertions objects compare too slow
    private static void Compare(string decompressedLevel, string actualLevel)
    {
        var expectedColors = GetColorsAsDictionary(decompressedLevel)?.ToArray();
        var actualColors = GetColorsAsDictionary(actualLevel)?.ToArray();

        if (expectedColors != null && actualColors != null)
        {
            actualColors.Should().HaveCount(expectedColors.Length);
            CompareDictionaries(expectedColors, actualColors, ColorDefaults);
        }

        var expectedBlocks = GetBlocksAsDictionary(decompressedLevel).ToArray();
        var actualBlocks = GetBlocksAsDictionary(actualLevel).ToArray();

        actualBlocks.Should().HaveCount(expectedBlocks.Length);

        CompareDictionaries(expectedBlocks, actualBlocks, BlockDefaults);
    }

    private static IEnumerable<Dictionary<string, string>> GetColorsAsDictionary(string level)
    {
        var levelParser = new LLParserSpan(";", level);
        var header = levelParser.Next().ToString();
        var headerParser = new LLParserSpan(",", header);
        string colors = null;
        while (headerParser.TryParseNext(out var key, out var valueSpan) && key.ToString() == "kS38" && !string.IsNullOrEmpty(colors = valueSpan.ToString()))
        {
        }

        if (colors == null)
            return null;

        var colorsParser = new LLParserSpan("|", colors);
        var list = new List<Dictionary<string, string>>();
        while (true)
        {
            var color = colorsParser.Next();
            if (color == null)
                break;
            list.Add(CreateDictionary(color.ToString().Split('_').Pairs()));
        }

        return list;
    }

    private static void CompareDictionaries(Dictionary<string, string>[] expectedBlocks, Dictionary<string, string>[] actualBlocks, Dictionary<string, string> defaults)
    {
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
                {
                    if (defaults.TryGetValue(first.Key, out var defaultValue) && first.Value == defaultValue)
                        continue;
                    Assert.Fail($"{first.Key} is disappeared. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
                }
                if (!CompareValue(first, second))
                    Assert.Fail($"{first.Key} value isn't same. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
            }

            foreach (var first in actual)
            {
                if (!expected.TryGetValue(first.Key, out var second))
                    Assert.Fail($"{first.Key} is appeared. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
                if (!CompareValue(first, second))
                    Assert.Fail(
                        $"{first.Key} value isn't same. {CreateExpectedAndActual(expectedBlocks[i], actualBlocks[i])}");
            }
        }
    }

    private static bool CompareValue(KeyValuePair<string, string> first, string second)
    {
        var same = first.Value.Equals(second, StringComparison.OrdinalIgnoreCase);
        // formats may be different in string representation
        if (!same && double.TryParse(first.Value, out var firstDouble) && double.TryParse(second, out var secondDouble))
            return Math.Abs(firstDouble - secondDouble) < 1e-8;
        return same;
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
            .Select(x => CreateDictionary(x.Pairs()));

    private static Dictionary<string, string> CreateDictionary(IEnumerable<KeyValuePair<string, string>> values)
    {
        var dict = new Dictionary<string, string>();
        foreach (var (key, value) in values)
            dict[key] = value;
        return dict;
    }

    private static void SaveFailed(string expected, string actual)
    {
        var failedTime = DateTime.UtcNow;
        var directory = Path.Combine("failed");
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        var fileExpected = Path.Combine(directory, $"{failedTime:yyyy-MM-ddThh:mm:ss}_{TestContext.CurrentContext.Test.Name}_expected");
        var fileResult = Path.Combine(directory, $"{failedTime:yyyy-MM-ddThh:mm:ss}_{TestContext.CurrentContext.Test.Name}_result");
        
        File.WriteAllLines(fileExpected, expected.Split(';').Take(TrimFailedLines ?? int.MaxValue));
        File.WriteAllLines(fileResult, actual.Split(';').Take(TrimFailedLines ?? int.MaxValue));
        
        TestContext.Out.WriteLine($"expected and actual values saved at '{Path.Combine(Environment.CurrentDirectory, directory)}'");
    }

    private static readonly int? TrimFailedLines = null;
}
