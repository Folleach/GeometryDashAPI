# GeometryDashAPI
Library for working with game data.<br>
Currently under development.
#### Such as:
- Levels (Colors, blocks and other).
- Game data in files.
- Game process.

### Download and use
1. Download binary file on the way: "[GeometryDashAPI/bin/Release/GeometryDashAPI.dll](https://github.com/Folleach/GeometryDashAPI/tree/master/GeometryDashAPI/bin/Release)".
2. Add a link to the file to the project.
3. Use.

### Examples
Levels and game data in files.
```cs
//Edit level description
LocalLevels local = new LocalLevels();
local.GetLevelByName("levelName").Description = "New description for level levelName";
local.Save();

//Get player name
GameManager gManager = new GameManager();
string playerName = gManager.PlayerName;
```
Levels edit.
```cs
Level level = new Level(local.GetLevelByName("Test"));
//Adding yellow color with ID 11
level.AddColor(new Color(11, 255, 255, 0));
//Creating a strip of blocks
for (int i = 0; i <= 900; i += 30)
{
    level.AddBlock(new Block(1)
    {
        PositionX = i,
        PositionY = i,
        ColorBase = 11
    });
}
//Save
local.GetLevelByName("Test").LevelString = level.ToString();
local.Save();
```
Result:<br>
![Result](https://raw.githubusercontent.com/Folleach/GeometryDashAPI/master/Images/LevelResultInReadme.png)<br><br>
Read and write process memory.
```cs
GameProcess process = new GameProcess();
process.Initialize(Access.PROCESS_VM_OPERATION | Access.PROCESS_VM_READ | Access.PROCESS_VM_WRITE);
IntPtr Player = process.Read<IntPtr>(process.GetModule("GeometryDash.exe"), new[] { 0x003222D0, 0x164, 0x224, 0x4E8, 0xB4 });
int AddressPositionX = IntPtr.Add(Player, 0x67C).ToInt32();
while (true)
{
    float posX = process.Read<float>(AddressPositionX);
    if (posX > 2500)
        process.Write<float>(AddressPositionX, 0);
    Console.WriteLine($"Position X: {posX}");
    Thread.Sleep(16);
}
```
