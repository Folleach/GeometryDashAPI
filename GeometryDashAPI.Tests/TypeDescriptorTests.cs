using System;
using System.Linq;
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
}
