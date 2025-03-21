﻿using System;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Serialization;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class BlockParseTest
{
    private IGameSerializer serializer = new ObjectSerializer();

    [Test]
    public void DecodeBlock_BaseBlock()
    {
        var input = "1,1,2,3,3,6";

        var actual = (Block)serializer.DecodeBlock(input.AsSpan());

        Assert.AreEqual(1, actual.Id);
        Assert.AreEqual(3, actual.PositionX);
        Assert.AreEqual(6, actual.PositionY);
    }

    [Test]
    public void EncodeBlock_BaseBlock()
    {
        var input = new BaseBlock(1)
        {
            PositionX = 44,
            PositionY = 77
        };
        var expected = "1,1,2,44,3,77";

        var actual = serializer.Encode(input).ToString();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void DecodeBlock_FullBaseBlock()
    {
        var input = "1,1,2,713,3,97,96,1,20,6,61,6,103,1,57,5.7.12,64,1,67,1,25,8,6,-90,21,9,24,-1,32,1.17,34,1,41,1,43,72a0.48a-0.64a1a1";

        var decoded = serializer.DecodeBlock(input.AsSpan());

        Assert.Pass("ok");
    }
}