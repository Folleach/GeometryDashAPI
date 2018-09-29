# GeometryDashAPI
Library for working with game data.<br>
#### Such as:
- Levels (Colors, blocks and other)
- Game data in files
- Game process

### Examples
Levels and game data in files
```cs
//Edit level description
LocalLevels local = new LocalLevels();
local.GetLevelByName("levelName").Description = "New description for level levelName";
local.Save();

//Get player name
GameManager gManager = new GameManager();
string playerName = gManager.PlayerName;
```
Levels edit
```cs
Level level = new Level(local.GetLevelByName("Test"));
//Adding yellow color with ID 11
level.Colors.AddColor(new Color(11)
{
    Red = 255,
    Green = 255,
    Blue = 0
});
//Creating a strip of blocks
for (int i = 0; i <= 900; i += 30)
{
    level.Blocks.Add(new Block(1)
    {
    PositionX = i,
    PositionY = i,
    ColorBase = 11
    });
}
//Saved
local.GetLevelByName("Test").LevelString = level.ToString();
local.Save();
```
Result:<br>
![Result](https://raw.githubusercontent.com/Folleach/GeometryDashAPI/master/Images/LevelResultInReadme.png)
