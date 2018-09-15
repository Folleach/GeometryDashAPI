# GeometryDashAPI
API for Geometry Dash in development


### Examples
Levels in the editor and game data
```cs
private void Method()
{
    //Edit level description
    LocalLevels local = new LocalLevels();
    local.GetLevelByName("levelName").Description = "New description for level levelName";
    local.Save();
    
    //Get player name
    GameManager gManager = new GameManager();
    string playerName = gManager.PlayerName;
}
```
