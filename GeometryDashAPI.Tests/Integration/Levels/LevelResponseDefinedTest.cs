using System.IO;
using NUnit.Framework;

namespace GeometryDashAPI.Tests.Integration.Levels;

[TestFixture(Explicit = true, Reason = "too sensitive")]
public class LevelResponseDefinedTest : LevelResponseTestBase
{
    [TestCase("12034598_Conclusion")]
    [TestCase("28755513_TheFinalLair")]
    [TestCase("116631_XmasParty")]
    public void LoadAndSaveLevel(string fileName)
    {
        var responseBody = File.ReadAllText(Path.Combine("data", "levels", fileName));
        Test(responseBody);
    }
}
