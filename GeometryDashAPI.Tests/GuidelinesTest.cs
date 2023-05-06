using FluentAssertions;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class GuidelinesTest
    {
        [Test]
        public void DecodeGuidelines()
        {
            var input = "1.000~0.9~1.255~1~4.525~0.8~6.665~0~";

            Guidelines actual = Guidelines.Parse(input);

            actual.Should().BeEquivalentTo(new Guidelines()
            {
                new Guideline()
                {
                    Timestamp = 1.000,
                    Color = GuidelineColor.Yellow
                },
                new Guideline()
                {
                    Timestamp = 1.255,
                    Color = GuidelineColor.Green
                },
                new Guideline()
                {
                    Timestamp = 4.525,
                    Color = GuidelineColor.Orange
                },
                new Guideline()
                {
                    Timestamp = 6.665,
                    Color = GuidelineColor.Orange
                },
            });
        }
    }
}
