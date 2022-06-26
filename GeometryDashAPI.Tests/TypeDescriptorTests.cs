using System.Linq;
using GeometryDashAPI.Parsers;
using NUnit.Framework;
using TestObjects;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class TypeDescriptorTests
{
    [Test]
    public void ShouldCreate()
    {
        var descriptor = new TypeDescriptor<ObjectSample, int>();

        var instance = descriptor.Create();
        
        Assert.AreNotEqual(null, instance);
    }

    [Test]
    public void ShouldCreateWithInit()
    {
        var descriptor = new TypeDescriptor<AllTypes, int>();

        var instance = descriptor.Create("1,abc,2,200");
        
        Assert.AreEqual("abc", instance.String);
        Assert.AreEqual(200, instance.Byte);
    }

    [Test]
    public void ShouldDecode()
    {
        var descriptor = new TypeDescriptor<ObjectSample, int>();

        var instance = descriptor.Create();
        descriptor.Set(instance, 33, "1.9");
        
        Assert.AreEqual(1.9, instance.X);
    }

    [Test]
    public void ShouldSaveToWithoutLoad()
    {
        var descriptor = new TypeDescriptor<ObjectSample, int>();
        
        var instance = descriptor.Create();
        descriptor.Set(instance, 11, "abc!");
        
        Assert.AreEqual(1, instance.WithoutLoaded.Count);
        Assert.AreEqual("11", instance.WithoutLoaded.FirstOrDefault().Key);
        Assert.AreEqual("abc!", instance.WithoutLoaded.FirstOrDefault().Value);
    }
}
