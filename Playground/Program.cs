// Hello! This is Geometry Dash API playground
// Here you can try your own code.
//
// See the wiki to learn how to use the library:
// https://github.com/Folleach/GeometryDashAPI/wiki
//
// Unfortunately, the RobTop server isn't working from GitHub
// Therefore, you cannot use GameClient.
//
// Press F5 to run this code

using GeometryDashAPI.Data;
using GeometryDashAPI.Levels.GameObjects.Specific;

var local = await LocalLevels.LoadFileAsync("GeometryDashAPI.Tests/data/saves/CCLocalLevels1.dat");

Console.WriteLine("this file contain levels:");
foreach (var item in local)
    Console.WriteLine($"\t{item.Name}");

var levelInfo = local.GetLevel("test1");
var level = levelInfo.LoadLevel();

Console.WriteLine($"level {levelInfo.Name} has {level.Blocks.Count} total blocks");

level.AddBlock(new JumpSphere(JumpSphereId.Yellow)
{
    PositionX = 100,
    PositionY = 100
});

levelInfo.SaveLevel(level);
await local.SaveAsync("data/CCLocalLevels.dat");

Console.WriteLine("level saved to data/CCLocalLevels.dat");
