using System;
using System.Linq;
using System.Text;
using FluentAssertions;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Specific;
using GeometryDashAPI.Serialization;
using NUnit.Framework;
using TestObjects;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class TypeDescriptorTests
{
    [Test]
    public void ShouldCreate()
    {
        var descriptor = new TypeDescriptor<ObjectSample>();

        var instance = descriptor.Create();
        
        Assert.AreNotEqual(null, instance);
    }

    [Test]
    public void ShouldCreateWithInit()
    {
        var descriptor = new TypeDescriptor<AllTypes>();

        var instance = descriptor.Create("1,abc,2,200".AsSpan());
        
        Assert.AreEqual("abc", instance.String);
        Assert.AreEqual(200, instance.Byte);
    }

    [Test]
    public void ShouldDecode()
    {
        var descriptor = new TypeDescriptor<ObjectSample>();

        var instance = descriptor.Create("33:1.9".AsSpan());
        
        Assert.AreEqual(1.9, instance.X);
    }

    [Test]
    public void ShouldSaveToWithoutLoad()
    {
        var descriptor = new TypeDescriptor<ObjectSample>();
        
        var instance = descriptor.Create("11:abc!".AsSpan());
        
        Assert.AreEqual(1, instance.WithoutLoaded.Count);
        Assert.AreEqual("11:abc!", instance.WithoutLoaded.FirstOrDefault());
    }

    [Test]
    public void ShouldParseEnumItself()
    {
        var descriptor = new TypeDescriptor<ObjectWithEnum>();
        
        var instance = descriptor.Create("1:33".AsSpan());
        
        Assert.AreEqual(SimpleEnum.X, instance.Value);
    }

    [Test]
    public void ProtectedVirtualFieldGet()
    {
        var descriptor = new TypeDescriptor<BaseBlock>();

        var builder = new StringBuilder();
        descriptor.CopyTo(new BaseBlock(1)
        {
            ZLayer = Layer.B4
        }, builder);

        builder.ToString().Should().Be("1,1,2,0,3,0,24,-3");
    }

    [Test]
    public void ProtectedVirtualFieldSet()
    {
        var input = "1,1,2,0,3,0,24,-3";
        var descriptor = new TypeDescriptor<BaseBlock>();

        var block = descriptor.Create(input.AsSpan());

        block.ZLayer.Should().Be(Layer.B4);
    }

    [Test]
    public void OverrideVirtualFieldGet_Default()
    {
        var descriptor = new TypeDescriptor<JumpPlate>();

        var builder = new StringBuilder();
        descriptor.CopyTo(new JumpPlate(JumpPlateId.Red)
        {
            ZLayer = Layer.B1
        }, builder);

        builder.ToString().Should().Be("1,1332,2,0,3,0");
    }

    [Test]
    public void OverrideVirtualFieldGet_Specific()
    {
        var descriptor = new TypeDescriptor<JumpPlate>();

        var builder = new StringBuilder();
        descriptor.CopyTo(new JumpPlate(JumpPlateId.Red)
        {
            ZLayer = Layer.B4
        }, builder);

        builder.ToString().Should().Be("1,1332,2,0,3,0,24,-3");
    }

    [Test]
    public void OverrideVirtualFieldSet_Default()
    {
        var input = "1,1332,2,0,3,0";
        var descriptor = new TypeDescriptor<JumpPlate>();

        var block = descriptor.Create(input.AsSpan());

        block.ZLayer.Should().Be(Layer.B1);
    }

    [Test]
    public void OverrideVirtualFieldSet_Specific()
    {
        var input = "1,1332,2,0,3,0,24,-3";
        var descriptor = new TypeDescriptor<JumpPlate>();

        var block = descriptor.Create(input.AsSpan());

        block.ZLayer.Should().Be(Layer.B4);
    }

    [Test]
    public void PrivateFieldFromInheritedClass()
    {
        var input = "1,10,2,20,3,333";
        var descriptor = new TypeDescriptor<InheritField>();

        var actual = descriptor.Create(input.AsSpan());

        actual.X.Should().Be(10);
        actual.Y.Should().Be(20);
    }
}
