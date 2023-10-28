using System.Text;
using GeometryDashAPI.Data;

namespace TestObjects;

public class GameManagerObjects
{
    public static GameManager CreateSample(int stringLength, int repeatTimes)
    {
        var keyZero = GenerateString(stringLength);
        var manager = GameManager.CreateNew();
        for (var i = 0; i < repeatTimes; i++)
            manager.DataPlist[$"KEY{i}"] = i == 0 ? keyZero : GenerateString(stringLength);
        return manager;
    }

    private static string GenerateString(int length)
    {
        var builder = new StringBuilder();
        var rand = new Random(123);
        for (var i = 0; i < length; i++)
            builder.Append((char)('a' + rand.Next('z' - 'a')));
        return builder.ToString();
    }
}
