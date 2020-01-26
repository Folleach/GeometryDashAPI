using GeometryDashAPI;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GameConvertTest
    {
        [TestCase("1", true, false)]
        [TestCase("0", false, false)]
        [TestCase("0", true, true)]
        [TestCase("1", false, true)]
        public void BoolToString(string expected, bool value, bool isReverse)
        {
            Assert.AreEqual(expected, GameConvert.BoolToString(value, isReverse));
        }

        [TestCase(true, "1", false)]
        [TestCase(false, "0", false)]
        [TestCase(false, "1", true)]
        [TestCase(true, "0", true)]
        public void StringToBool(bool expected, string value, bool isReverse)
        {
            Assert.AreEqual(expected, GameConvert.StringToBool(value, isReverse));
        }

        [TestCase("1", 1f)]
        [TestCase("1.33", 1.33f)]
        public void SingleToString(string expected, float value)
        {
            Assert.AreEqual(expected, GameConvert.SingleToString(value));
        }

        [TestCase(1f, "1")]
        [TestCase(1.33f, "1.33")]
        public void StringToSingle(float expected, string value)
        {
            Assert.AreEqual(expected, GameConvert.StringToSingle(value));
        }
    }
}
