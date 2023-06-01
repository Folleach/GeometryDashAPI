using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
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

    public async Task<Level> GetLevelAsync(OfficialLevel officialLevel)
    {
        var gameType = officialLevel.GetGameType();
        if (gameType == GameType.Meltdown)
            throw new InvalidEnumArgumentException("meltdown levels is not supported");
        if (!levels.TryGetValue(gameType, out var plist))
        {
            var bytes = await File.ReadAllBytesAsync(Path.Combine(path, GetLevelDataPath(gameType)));
            levels.TryAdd(gameType, plist = new Plist(bytes));
        }

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
