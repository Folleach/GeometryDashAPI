using System;
using FluentAssertions;
using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class GameResourcesTests
{
    private const string ResourcesDataPathEnv = "GDAPI_TESTS_RESOURCES";

    private string path;

    [SetUp]
    public void SetUp()
    {
        path = Environment.GetEnvironmentVariable(ResourcesDataPathEnv) 
               ?? Environment.GetEnvironmentVariable(ResourcesDataPathEnv, EnvironmentVariableTarget.User);
        if (path == null)
            throw new InvalidOperationException($"Please set env variable '{ResourcesDataPathEnv}' pointing to the game resource files");
    }

    [TestCase(OfficialLevel.StereoMadness)]
    [TestCase(OfficialLevel.BackOnTrack)]
    [TestCase(OfficialLevel.Polargeist)]
    [TestCase(OfficialLevel.DryOut)]
    [TestCase(OfficialLevel.BaseAfterBase)]
    [TestCase(OfficialLevel.CantLetGo)]
    [TestCase(OfficialLevel.Jumper)]
    [TestCase(OfficialLevel.TimeMachine)]
    [TestCase(OfficialLevel.Cycles)]
    [TestCase(OfficialLevel.xStep)]
    [TestCase(OfficialLevel.Clutterfunk)]
    [TestCase(OfficialLevel.TheoryOfEverything)]
    [TestCase(OfficialLevel.ElectromanAdventures)]
    [TestCase(OfficialLevel.Clubstep)]
    [TestCase(OfficialLevel.Electrodynamix)]
    [TestCase(OfficialLevel.HexagonForce)]
    [TestCase(OfficialLevel.BlastProcessing)]
    [TestCase(OfficialLevel.TheoryOfEverything2)]
    [TestCase(OfficialLevel.GeometricalDominator)]
    [TestCase(OfficialLevel.Deadlocked)]
    [TestCase(OfficialLevel.Fingerdash)]

    [TestCase(OfficialLevel.TheChallenge)]

    [TestCase(OfficialLevel.TheSevenSeas)]
    [TestCase(OfficialLevel.VikingArena)]
    [TestCase(OfficialLevel.AirborneRobots)]

    [TestCase(OfficialLevel.Payload)]
    [TestCase(OfficialLevel.BeastMode)]
    [TestCase(OfficialLevel.Machina)]
    [TestCase(OfficialLevel.Years)]
    [TestCase(OfficialLevel.Frontlines)]
    [TestCase(OfficialLevel.SpacePirates)]
    [TestCase(OfficialLevel.Striker)]
    [TestCase(OfficialLevel.Embers)]
    [TestCase(OfficialLevel.Round1)]
    [TestCase(OfficialLevel.MonsterDanceOff)]
    public void GetLevel_ShouldParseCorrect(OfficialLevel officialLevel)
    {
        var resources = new GameResources(path);

        Level level = null;
        Assert.DoesNotThrow(() => level = resources.GetLevel(officialLevel));

        level.Should().NotBeNull();
        level.Blocks.Should().HaveCountGreaterThan(0);
    }
}

