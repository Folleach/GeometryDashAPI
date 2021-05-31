using System;
using FluentAssertions;
using GeometryDashAPI.Parser;
using GeometryDashAPI.Tests.TestObjects;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    public class ObjectParserTests
    {
        [TestCase("33:1.4", 1.4)]
        [TestCase("11:1:33:4", 4)]
        public void Decode_SampleObject_ShouldDecodeCorrect(string raw, double expected)
        {
            var actual = ObjectParser.Decode<Sample>(raw);
            
            Assert.AreEqual(expected, actual.X);
        }

        [Test]
        public void Decode_SampleObject_ExtraPropertiesShouldBeSave()
        {
            var input = "10:hello:33:9";

            var actual = ObjectParser.Decode<Sample>(input);
            
            Assert.AreEqual(9, actual.X);
            Assert.AreEqual(1, actual.WithoutLoaded.Count);
            Assert.AreEqual("hello", actual.WithoutLoaded["10"]);
        }

        [TestCase("10:33:10")]
        [TestCase("1")]
        public void Decode_SampleObject_ThrowExceptionIfHaveError(string input)
        {
            Assert.Throws(typeof(Exception), () => ObjectParser.Decode<Sample>(input));
        }
        
        [TestCase("33:1.4")]
        [TestCase("33:4:11:1")]
        public void Decode_SampleObject_ShouldEncodeCorrect(string raw)
        {
            var decoded = ObjectParser.Decode<Sample>(raw);
            var encoded = ObjectParser.Encode(decoded);
            
            Assert.AreEqual(raw, encoded);
        }
        
        [Test]
        public void Decode_SampleContainer_ShouldRecursiveDecodeCorrect()
        {
            var expected = new SampleContainer()
            {
                Sample1 = new Sample() { X = 1.3 },
                Sample2 = new Sample() { X = 11 }
            };
            const string input = "1~33:1.3~2~33:11";
            
            var decoded = ObjectParser.Decode<SampleContainer>(input);
            
            decoded.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.ParserSense));
        }
        
        [Test]
        public void Decode_SampleContainer_ShouldRecursiveEncodeCorrect()
        {
            var input = new SampleContainer()
            {
                Sample1 = new Sample() { X = 1.3 },
                Sample2 = new Sample() { X = 11 }
            };
            const string expected = "1~33:1.3~2~33:11";
            
            var encoded = ObjectParser.Encode(input);
            
            Assert.AreEqual(expected, encoded);
        }

        [Test]
        public void Encode_AllTypes_ShouldNotThrowException()
        {
            var all = new AllTypes();
            Assert.DoesNotThrow(() => ObjectParser.Encode(all));
        }
        
        [Test]
        public void Decode_AllTypes_ShouldNotThrowException()
        {
            var all = ObjectParser.Encode(new AllTypes());
            Assert.DoesNotThrow(() => ObjectParser.Decode<AllTypes>(all));
        }
    }
}