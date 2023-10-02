using System;
using FluentAssertions;
using GeometryDashAPI.Serialization;
using NUnit.Framework;
using TestObjects;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class StructParserTests
{
    private static ObjectSerializer serializer = new();

    [Test]
    public void Decode_SampleStruct_ShouldBeCorrect()
    {
        var input = "33:1~8:444";

        var actual = serializer.Decode<StructSample>(input.AsSpan());

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

        var actual = serializer.Decode<ComplexParserObject>(input.AsSpan());

        actual.Should().BeEquivalentTo(expected);
    }
}
