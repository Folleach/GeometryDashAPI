using System;
using System.Linq;
using FluentAssertions;
using GeometryDashAPI.Parsers;
using NUnit.Framework;
using TestObjects;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class ObjectParserTests
    {
        private IGameParser parser = new ObjectParser();

        [TestCase("33:1.4", 1.4)]
        [TestCase("11:1:33:4", 4)]
        public void Decode_SampleObject_ShouldDecodeCorrect(string raw, double expected)
        {
            var actual = parser.Decode<ObjectSample>(raw);
            
            Assert.AreEqual(expected, actual.X);
        }

        [Test]
        public void Decode_SampleObject_ExtraPropertiesShouldBeSave()
        {
            var input = "10:hello:33:9";

            var actual = parser.Decode<ObjectSample>(input);
            
            Assert.AreEqual(9, actual.X);
            Assert.AreEqual(1, actual.WithoutLoaded.Count);
            Assert.AreEqual("10:hello", actual.WithoutLoaded.FirstOrDefault());
        }

        [TestCase("10:33:10")]
        [TestCase("1")]
        public void Decode_SampleObject_ThrowExceptionIfHaveError(string input)
        {
            Assert.Throws(typeof(Exception), () => parser.Decode<ObjectSample>(input));
        }
        
        [TestCase("33:1.4")]
        [TestCase("33:4:11:1")]
        [Ignore("Not implemented")]
        public void Decode_SampleObject_ShouldEncodeCorrect(string raw)
        {
            var decoded = parser.Decode<ObjectSample>(raw);
            // var encoded = parser.Encode(decoded);
            
            // Assert.AreEqual(raw, encoded);
        }
        
        [Test]
        public void Decode_SampleContainer_ShouldRecursiveDecodeCorrect()
        {
            var expected = new SampleContainer()
            {
                Sample1 = new ObjectSample() { X = 1.3 },
                Sample2 = new ObjectSample() { X = 11 }
            };
            const string input = "1~33:1.3~2~33:11";
            
            var decoded = parser.Decode<SampleContainer>(input);
            
            decoded.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        [Ignore("Not implemented")]
        public void Decode_SampleContainer_ShouldRecursiveEncodeCorrect()
        {
            var input = new SampleContainer()
            {
                Sample1 = new ObjectSample() { X = 1.3 },
                Sample2 = new ObjectSample() { X = 11 }
            };
            const string expected = "1~33:1.3~2~33:11";
            
            // var encoded = parser.Encode(input);
            
            // Assert.AreEqual(expected, encoded);
        }

        [Test]
        [Ignore("Not implemented")]
        public void Encode_AllTypes_ShouldNotThrowException()
        {
            // var all = new AllTypes();
            // Assert.DoesNotThrow(() => parser.Encode(all));
        }
        
        [Test]
        [Ignore("Not implemented")]
        public void Decode_AllTypes_ShouldNotThrowException()
        {
            // var all = parser.Encode(new AllTypes());
            // Assert.DoesNotThrow(() => parser.Decode<AllTypes>(all));
        }

        [Test]
        public void Decode_MultipleSense_ShouldBeCorrect()
        {
            var input = "1~|~2";
            var expected = new MultipleSense()
            {
                X1 = 2
            };

            var actual = parser.Decode<MultipleSense>(input);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetArray()
        {
            var actual = parser.GetArray("33,1,33,2,33,3,33,4,33,5,33,6,33,7,33,8,33,9", ",", Parsers.Parsers.GetOrDefault_Int32__);

            var expected = new[] { 33,1,33,2,33,3,33,4,33,5,33,6,33,7,33,8,33,9 };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}