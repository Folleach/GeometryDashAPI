using System;
using System.Collections.Generic;
using System.IO;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Data;

public class GameResources
{
    private readonly string path;
    private Dictionary<GameType, Plist> levels = new();

    public GameResources(string path)
    {
        this.path = path;
    }

    public Level GetLevel(OfficialLevel officialLevel)
    {
        var gameType = officialLevel.GetGameType();
        var plist = levels.GetOrCreate(
            gameType,
            version => new Plist(File.ReadAllBytes(Path.Combine(path, GetLevelDataPath(version)))));

        var content = plist[((int)officialLevel).ToString()].ToString();
        if (gameType == GameType.Default)
            return new Level($"H4sIAAAAAAAAC{content}", compressed: true);
        return new Level(content, compressed: true);
    }

    private static string GetLevelDataPath(GameType type)
    {
        return type switch
        {
            GameType.Default => "LevelData.plist",
            GameType.World => "LevelDataWorld.plist",
            GameType.Meltdown => "LevelDataMeltdown.plist",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
