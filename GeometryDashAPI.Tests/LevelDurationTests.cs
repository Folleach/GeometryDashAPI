using System.IO;
using System.Net;
using FluentAssertions;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Responses;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class LevelDurationTests
{
    [TestCase("12034598_Conclusion", 57)]
    [TestCase("28755513_TheFinalLair", 154)]
    [TestCase("116631_XmasParty", 87)]
    public void Test(string fileName, int expectedSeconds)
    {
        var responseBody = File.ReadAllText(Path.Combine("data", "levels", fileName));
        var response = new ServerResponse<LevelResponse>(HttpStatusCode.OK, responseBody);
        var level = new Level(response.GetResultOrDefault().Level.LevelString, compressed: true);
        level.Duration.TotalSeconds.Should().BeApproximately(expectedSeconds, precision: 1);
    }
}
