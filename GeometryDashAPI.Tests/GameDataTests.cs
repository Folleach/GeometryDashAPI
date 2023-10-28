using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Serialization;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class GameDataTests
{
    private const string PROCESS_TEMP_DATA_DIRECTORY = "GameDataTemp";

    private static readonly string GameManager1Path = Path.Combine("data", "saves", "CCGameManager1.dat");
    private static readonly string LocalLevels1Path = Path.Combine("data", "saves", "CCLocalLevels1.dat");

    private static readonly string GameManagerEmptyPath = Path.Combine("data", "saves", "CCGameManagerEmpty.dat");
    private static readonly string LocalLevelsEmptyPath = Path.Combine("data", "saves", "CCLocalLevelsEmpty.dat");

    [SetUp]
    public void SetUp()
    {
        if (Directory.Exists(PROCESS_TEMP_DATA_DIRECTORY))
            Directory.Delete(PROCESS_TEMP_DATA_DIRECTORY, true);
        Directory.CreateDirectory(PROCESS_TEMP_DATA_DIRECTORY);
    }

    [TearDown]
    public void TearDown()
    {
        Directory.Delete(PROCESS_TEMP_DATA_DIRECTORY, true);
    }

    [Test]
    public void LocalLevels_Load()
    {
        var local = LocalLevels.LoadFile(LocalLevels1Path);

        local.LevelExists("test1", 0).Should().Be(true);
        local.GetLevel("test1").Description.Should().Be("for tests");
    }

    [Test]
    public async Task LocalLevels_LoadAsync()
    {
        var local = await LocalLevels.LoadFileAsync(LocalLevels1Path);

        local.LevelExists("test1", 0).Should().Be(true);
        local.GetLevel("test1").Description.Should().Be("for tests");
    }

    [Test]
    public async Task LocalLevels_SaveAsync()
    {
        var local = await LocalLevels.LoadFileAsync(LocalLevels1Path);
        local.GetLevel("test1").Description = "new description";

        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "SaveAsync.dat");
        await local.SaveAsync(fileName);

        local = await LocalLevels.LoadFileAsync(fileName);
        local.GetLevel("test1").Description.Should().Be("new description");
    }

    [Test]
    public async Task LocalLevels_Save()
    {
        var local = await LocalLevels.LoadFileAsync(LocalLevels1Path);
        local.GetLevel("test1").Description = "new description";

        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "Save.dat");
        // ReSharper disable once MethodHasAsyncOverload
        local.Save(fileName);

        local = await LocalLevels.LoadFileAsync(fileName);
        local.GetLevel("test1").Description.Should().Be("new description");
    }

    [Test]
    public void GameManager_Load()
    {
        var game = GameManager.LoadFile(GameManager1Path);

        game.PlayerCube.Should().Be(1);
    }

    [Test]
    public async Task GameManager_LoadAsync()
    {
        var game = await GameManager.LoadFileAsync(GameManager1Path);

        game.PlayerCube.Should().Be(1);
    }

    [Test]
    public async Task GameManager_SaveAsync()
    {
        var game = await GameManager.LoadFileAsync(GameManager1Path);
        game.PlayerCube = 33;
        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "GameManagerSaveAsync.dat");
        await game.SaveAsync(fileName);

        game = await GameManager.LoadFileAsync(fileName);
        game.PlayerCube.Should().Be(33);
    }

    [Test]
    public async Task GameManager_Save()
    {
        var game = await GameManager.LoadFileAsync(GameManager1Path);
        game.PlayerCube = 33;
        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "GameManagerSaveAsync.dat");
        // ReSharper disable once MethodHasAsyncOverload
        game.Save(fileName);

        game = await GameManager.LoadFileAsync(fileName);
        game.PlayerCube.Should().Be(33);
    }

    [Test]
    public async Task LocalLevels_SpecificSymbols()
    {
        var input = "& < > / \\ @";
        var local = await LocalLevels.LoadFileAsync(LocalLevels1Path);
        local.GetLevel("test1").Name = input;

        var path = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "SpecificSymbols.dat");
        await local.SaveAsync(path);
        local = await LocalLevels.LoadFileAsync(path);

        local.LevelExists("test1").Should().BeFalse();
        local.FirstOrDefault()?.Name.Should().Be(input);
    }

    [Test]
    public async Task LocalLevels_CreateNew()
    {
        var local = await LocalLevels.LoadFileAsync(LocalLevelsEmptyPath);
        var localNew = LocalLevels.CreateNew();

        local.DataPlist.Should().BeEquivalentTo(localNew.DataPlist);
    }

    [Test]
    public async Task LocalLevels_CreateNewWithLevel()
    {
        var local = await LocalLevels.LoadFileAsync(LocalLevels1Path);
        var localNew = LocalLevels.CreateNew();

        // because this properties isn't present in new level
        local.GetLevel("test1").DataLevel.Remove("k3");
        local.GetLevel("test1").DataLevel.Remove("k47");
        local.GetLevel("test1").DataLevel.Remove("k48");
        local.GetLevel("test1").DataLevel.Remove("k80");

        // restore to default in prepared file
        local.GetLevel("test1").DataLevel["kI1"] = 0;
        local.GetLevel("test1").DataLevel["kI2"] = 0;
        local.GetLevel("test1").DataLevel["kI3"] = 0;
        (local.GetLevel("test1").DataLevel["kI6"] as Plist)?.Clear();

        var level = new Level();

        localNew.AddLevel(LevelCreatorModel.CreateNew("test1", "Player", level));
        local.GetLevel("test1").SaveLevel(level);

        local.DataPlist.Should().BeEquivalentTo(localNew.DataPlist);
    }

    [Test]
    public async Task GameManager_CreateNew()
    {
        var manager = await GameManager.LoadFileAsync(GameManagerEmptyPath);
        var managerNew = GameManager.CreateNew();

        managerNew.DataPlist["playerUDID"] = manager.DataPlist["playerUDID"];

        manager.DataPlist.Should().BeEquivalentTo(managerNew.DataPlist);
    }

    [Test]
    public void LocalLevels_RemoveTest()
    {
        var local = LocalLevels.CreateNew();
        var toRemove = LevelCreatorModel.CreateNew("test1", "me");

        local.AddLevel(toRemove);
        local.AddLevel(LevelCreatorModel.CreateNew("test2", "me"));
        
        local.Remove(toRemove).Should().BeFalse();
        local.LevelExists("test1").Should().BeTrue();

        local.Remove(local.GetLevel("test1")).Should().BeTrue();
        local.LevelExists("test1").Should().BeFalse();
    }

    [Test]
    public void Data_SaveAndLoadLargeFile()
    {
        const int stringLength = 1024 * 1024;
        var keyZero = GenerateString(stringLength);
        var expectedManager = GameManager.CreateNew();
        for (var i = 0; i < 10; i++)
            expectedManager.DataPlist[$"KEY{i}"] = i == 0 ? keyZero : GenerateString(stringLength);
        using var file = FileContext.Create(removeAfterDispose: false);

        expectedManager.Save(file.Name);
        var actualManager = GameManager.LoadFile(file.Name);

        Assert.AreEqual(keyZero, actualManager.DataPlist["KEY0"]);
    }

    private string GenerateString(int length)
    {
        var builder = new StringBuilder();
        var rand = new Random(123);
        for (var i = 0; i < length; i++)
            builder.Append((char)('a' + rand.Next('z' - 'a')));
        return builder.ToString();
    }
}
