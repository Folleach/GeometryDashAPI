using FluentAssertions;
using GeometryDashAPI.Parsers;
using NUnit.Framework;
using TestObjects;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class StructParserTests
    {
        [Test]
        public void Decode_SampleStruct_ShouldBeCorrect()
        {
            var input = "33:1~8:444";

            var actual = StructParser.Decode<StructSample>(input);

            var expected = new StructSample()
            {
                FirstObject = new ObjectSample()
                {
                    X = 1
                },
                SecondObject = new LargeObject()
                {
                    x8 = 444
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Decode_ComplexParserObject_ShouldBeCorrect()
        {
            var input = ComplexParserObject.ExampleInput;
            var expected = ComplexParserObject.ExampleExpected;

            var actual = StructParser.Decode<ComplexParserObject>(input);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}