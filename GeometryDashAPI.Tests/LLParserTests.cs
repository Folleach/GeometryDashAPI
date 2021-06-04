using GeometryDashAPI.Parsers;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class LLParserTests
    {
        [TestCase("", 0)]
        [TestCase("abc", 1)]
        [TestCase("1.1", 2)]
        [TestCase("1.2.3.4", 4)]
        [TestCase("1.2.3.", 3)]
        [TestCase("..", 2)]
        public void Next_CallNumbers(string input, int expectedCallNumber)
        {
            var parser = new LLParser('.', input);

            var calls = 0;
            while (parser.Next() != null)
                calls++;
            
            Assert.AreEqual(expectedCallNumber, calls);
        }
    }
}