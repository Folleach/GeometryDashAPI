using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Enums;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class GameServerTests
{
    [Test]
    public async Task GetFeatures()
    {
        var server = new GameServer();
        var response = await server.GetLevels(new GetLevelsQuery(SearchType.Featured));
        var result = response.GetResultOrDefault();

        response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Page.TotalCount.Should().BeGreaterThan(28000); // approximate number of featured levels
        result.Page.CountOnPage.Should().Be(10);
        result.Page.RangeIn.Should().Be(0);
        result.Page.RangeOut.Should().Be(10);
        result.Levels.Count.Should().Be(10);
        result.WithoutLoaded.Should().HaveCount(0);
    }

    [Test]
    public async Task SearchAccount()
    {
        var server = new GameServer();
        var response = await server.GetUserByName("Folleach");
        var result = response.GetResultOrDefault();

        response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.User.Name.Should().Be("Folleach");
        result.WithoutLoaded.Should().HaveCount(0);
        result.User.WithoutLoaded.Should().HaveCount(0);

        var accountResponse = await server.GetAccountInfo(result.User.AccountId);
        var accountResult = accountResponse.GetResultOrDefault();

        accountResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        accountResult.Should().NotBeNull();
        accountResult.Account.AccountId.Should().Be(result.User.AccountId);
        accountResult.Account.Name.Should().Be(result.User.Name);
        accountResult.WithoutLoaded.Should().HaveCount(0);
        accountResult.Account.WithoutLoaded.Should().HaveCount(0);
    }
}
