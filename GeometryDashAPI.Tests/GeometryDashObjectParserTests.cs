using System;
using GeometryDashAPI.Tests.TestObjects;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    public class GeometryDashObjectParserTests
    {
        [TestCase("33:1.4", 1.4)]
        [TestCase("11:1:33:4", 4)]
        public void Decode_SampleObject_ShouldDecodeCorrect(string raw, double expected)
        {
            var actual = GeometryDashApi.GetObjectParser().Decode<Sample>(raw);
            
            Assert.AreEqual(expected, actual.X);
        }

        [Test]
        public void Decode_SampleObject_ExtraPropertiesShouldBeSave()
        {
            var input = "10:hello:33:9";

            var actual = GeometryDashApi.GetObjectParser().Decode<Sample>(input);
            
            Assert.AreEqual(9, actual.X);
            Assert.AreEqual(1, actual.WithoutLoaded.Count);
            Assert.AreEqual("hello", actual.WithoutLoaded["10"]);
        }

        [TestCase("10:33:10")]
        [TestCase("1")]
        public void Decode_SampleObject_ThrowExceptionIfHaveError(string input)
        {
            Assert.Throws(typeof(Exception), () => GeometryDashApi.GetObjectParser().Decode<Sample>(input));
        }
        
        [TestCase("33:1.4")]
        [TestCase("33:4:11:1")]
        public void Decode_SampleObject_ShouldEncodeCorrect(string raw)
        {
            var decoded = GeometryDashApi.GetObjectParser().Decode<Sample>(raw);
            var encoded = GeometryDashApi.GetObjectParser().Encode(decoded);
            
            Assert.AreEqual(raw, encoded);
        }
    }
}