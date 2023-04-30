using System;
using System.Linq;
using FluentAssertions;
using GeometryDashAPI.Serialization;
using NUnit.Framework;
using TestObjects;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class ObjectParserTests
{
    private IGameSerializer serializer = new ObjectSerializer();

    [TestCase("33:1.4", 1.4)]
    [TestCase("11:1:33:4", 4)]
    public void Decode_SampleObject_ShouldDecodeCorrect(string raw, double expected)
    {
        var actual = serializer.Decode<ObjectSample>(raw);

        Assert.AreEqual(expected, actual.X);
    }

    [Test]
    public void Decode_SampleObject_ExtraPropertiesShouldBeSave()
    {
        var input = "10:hello:33:9";

        var actual = serializer.Decode<ObjectSample>(input);
            
        Assert.AreEqual(9, actual.X);
        Assert.AreEqual(1, actual.WithoutLoaded.Count);
        Assert.AreEqual("10:hello", actual.WithoutLoaded.FirstOrDefault());
    }

    [TestCase("10:33:10")]
    [TestCase("1")]
    public void Decode_SampleObject_ThrowExceptionIfHaveError(string input)
    {
        Assert.Throws(typeof(Exception), () => serializer.Decode<ObjectSample>(input));
    }
        
    [TestCase("33:1.4")]
    [TestCase("33:4:11:1")]
    public void Decode_SampleObject_ShouldEncodeCorrect(string raw)
    {
        var decoded = serializer.Decode<ObjectSample>(raw);
        var encoded = serializer.Encode(decoded).ToString();
        Assert.AreEqual(raw, encoded);
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
            
        var decoded = serializer.Decode<SampleContainer>(input);
            
        decoded.Should().BeEquivalentTo(expected);
    }
        
    [Test]
    public void Decode_SampleContainer_ShouldRecursiveEncodeCorrect()
    {
        var input = new SampleContainer()
        {
            Sample1 = new ObjectSample() { X = 1.3 },
            Sample2 = new ObjectSample() { X = 11 }
        };
        const string expected = "1~33:1.3~2~33:11";

        var encoded = serializer.Encode(input).ToString();

        Assert.AreEqual(expected, encoded);
    }

    [Test]
    public void Encode_AllTypes_ShouldNotThrowException()
    {
        var all = new AllTypes();
        Assert.DoesNotThrow(() => serializer.Encode(all));
    }

    [Test]
    public void Decode_AllTypes_ShouldNotThrowException()
    {
        Assert.DoesNotThrow(() => serializer.Decode<AllTypes>(serializer.Encode(new AllTypes())));
    }

    [Test]
    public void Decode_MultipleSense_ShouldBeCorrect()
    {
        var input = "1~|~2";
        var expected = new MultipleSense()
        {
            X1 = 2
        };

        var actual = serializer.Decode<MultipleSense>(input);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetArray()
    {
        var actual = serializer.GetArray("33,1,33,2,33,3,33,4,33,5,33,6,33,7,33,8,33,9", ",", Parsers.GetOrDefault_Int32__);

        var expected = new[] { 33,1,33,2,33,3,33,4,33,5,33,6,33,7,33,8,33,9 };

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void NullableField_Encode_HasValue()
    {
        var input = new WithNullable()
        {
            Value = 12
        };

        var actual = serializer.Encode(input).ToString();

        actual.Should().Be("3:12");
    }
    
    [Test]
    public void NullableField_Decode_HasValue()
    {
        var input = "3:44";

        var actual = serializer.Decode<WithNullable>(input);

        actual.Value.Should().Be(44);
    }

    [Test]
    public void NullableField_Encode_Null()
    {
        var input = new WithNullable()
        {
            Value = null
        };

        var actual = serializer.Encode(input).ToString();

        actual.Should().BeEmpty();
    }

    [Test]
    public void NullableField_Encode_Zero()
    {
        var input = new WithNullable()
        {
            Value = 0
        };

        var actual = serializer.Encode(input).ToString();

        actual.Should().Be("3:0");
    }
}
