using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using GeometryDashAPI.Data;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class GameDataTests
{
    private const string PROCESS_TEMP_DATA_DIRECTORY = "GameDataTemp";

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
        var local = new LocalLevels(Path.Combine("data", "saves", "CCLocalLevels1.dat"));

        local.LevelExists("test1", 0).Should().Be(true);
        local.GetLevel("test1").Description.Should().Be("for tests");
    }

    [Test]
    public async Task LocalLevels_LoadAsync()
    {
        var local = await LocalLevels.LoadFileAsync(Path.Combine("data", "saves", "CCLocalLevels1.dat"));

        local.LevelExists("test1", 0).Should().Be(true);
        local.GetLevel("test1").Description.Should().Be("for tests");
    }

    [Test]
    public async Task LocalLevels_SaveAsync()
    {
        var local = await LocalLevels.LoadFileAsync(Path.Combine("data", "saves", "CCLocalLevels1.dat"));
        local.GetLevel("test1").Description = "new description";

        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "SaveAsync.dat");
        await local.SaveAsync(fileName);

        local = await LocalLevels.LoadFileAsync(fileName);
        local.GetLevel("test1").Description.Should().Be("new description");
    }

    [Test]
    public async Task LocalLevels_Save()
    {
        var local = await LocalLevels.LoadFileAsync(Path.Combine("data", "saves", "CCLocalLevels1.dat"));
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
        var game = new GameManager(Path.Combine("data", "saves", "CCGameManager1.dat"));

        game.PlayerCube.Should().Be(1);
    }

    [Test]
    public async Task GameManager_LoadAsync()
    {
        var game = await GameManager.LoadFileAsync(Path.Combine("data", "saves", "CCGameManager1.dat"));

        game.PlayerCube.Should().Be(1);
    }

    [Test]
    public async Task GameManager_SaveAsync()
    {
        var game = await GameManager.LoadFileAsync(Path.Combine("data", "saves", "CCGameManager1.dat"));
        game.PlayerCube = 33;
        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "GameManagerSaveAsync.dat");
        await game.SaveAsync(fileName);

        game = await GameManager.LoadFileAsync(fileName);
        game.PlayerCube.Should().Be(33);
    }

    [Test]
    public async Task GameManager_Save()
    {
        var game = await GameManager.LoadFileAsync(Path.Combine("data", "saves", "CCGameManager1.dat"));
        game.PlayerCube = 33;
        var fileName = Path.Combine(PROCESS_TEMP_DATA_DIRECTORY, "GameManagerSaveAsync.dat");
        // ReSharper disable once MethodHasAsyncOverload
        game.Save(fileName);

        game = await GameManager.LoadFileAsync(fileName);
        game.PlayerCube.Should().Be(33);
    }
}
