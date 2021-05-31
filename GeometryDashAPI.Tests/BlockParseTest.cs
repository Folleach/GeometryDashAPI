using System;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Parser;
using GeometryDashAPI.Parsers;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class BlockParseTest
    {
        [Test]
        public void DecodeBlock_BaseBlock()
        {
            var input = "1,1,2,3,3,6";

            var actual = ObjectParser.DecodeBlock(input);

            Assert.AreEqual(1, actual.ID);
            Assert.AreEqual(3, actual.PositionX);
            Assert.AreEqual(6, actual.PositionY);
        }

        [Test]
        public void EncodeBlock_BaseBlock()
        {
            var input = new BaseBlock(1)
            {
                PositionX = 44,
                PositionY = 77
            };
            var expected = "1,1,2,44,3,77";

            var actual = ObjectParser.EncodeBlock(input);
            
            Assert.AreEqual(expected, actual);
        }
    }
}