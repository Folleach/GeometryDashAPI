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

[TestFixture]
public class LevelResponseTest
{
    private const string PROCESS_DATA_PATH_ENV = "GDAPI_TESTS_CONTENTS";
    private const string PROCESS_LEVELCONTENT_FILENAME = "z";

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
            $"'{nameof(result.WithoutLoaded)}' in _level preview contains any elements. It's wrong for full compability");

        var decompressedLevel = Level.Decompress(result.Level.LevelString);
        var arr = decompressedLevel.Split(';', 2);
        var level = new Level(decompressedLevel, false);

        var serializedLevel = level.ToString();
        decompressedLevel.Should().Be(serializedLevel);
    }
}
