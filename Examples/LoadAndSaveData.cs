using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using System;

namespace Examples
{
    // An example of creating a load and save game data.
    public class LoadAndSaveData
    {
        public static void Invoke()
        {
            { // Loading and saving local levels (In editor).
                LocalLevels levels = new LocalLevels();
                levels.Save();
            }
            { // Loading and saving game manager (UserName, music list, saved levels...).
                GameManager manager = new GameManager();
                manager.Save("C:\\temp\\gameManager.dat");
            }
            { // Loading and saving other files.
                GameData data = new GameData("C:\\temp\\other.dat");
                if (data.Save(true))
                    Console.WriteLine("Saved.");
                else
                    Console.WriteLine("Save failed, close all game instances.");
            }
            { // Loading and saving level.
                LocalLevels levels = new LocalLevels();
                Level lvl = new Level(levels.GetLevel("Level name"));
                levels.GetLevel("Level name").LevelString = lvl.ToString();
                levels.Save();
            }
        }
    }
}
