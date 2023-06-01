using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;

namespace Examples
{
    // An example of creating a load and save game data.
    public class LoadAndSaveData
    {
        public static void Invoke()
        {
            { // Loading and saving local levels (In editor).
                LocalLevels levels = LocalLevels.LoadFile();
                levels.Save();
            }
            { // Loading and saving game manager (UserName, music list, saved levels...).
                GameManager manager = GameManager.LoadFile();
                manager.Save("C:\\temp\\gameManager.dat");
            }
            { // Loading and saving other files.
                GameData data = new GameData();
                data.LoadAsync("C:\\temp\\other.dat").GetAwaiter().GetResult();
                data.Save();
            }
            { // Loading and saving level.
                LocalLevels levels = LocalLevels.LoadFile();
                Level lvl = new Level(levels.GetLevel("Level name"));
                levels.GetLevel("Level name").SaveLevel(lvl);
                levels.Save();
            }
        }
    }
}
