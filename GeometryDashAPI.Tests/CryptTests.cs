using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class CryptTests
{
    [Test]
    public void InlineXor()
    {
        var random = new Random(123);
        var data = Enumerable.Range(0, 100).Select(_ => (byte)random.Next(byte.MaxValue)).ToArray();
        var expected = (byte[])data.Clone();

        Crypt.NaiveXor(expected, 11);
        Crypt.InlineXor(data.AsSpan(), 11);

        data.Should().BeEquivalentTo(expected);
    }
}
