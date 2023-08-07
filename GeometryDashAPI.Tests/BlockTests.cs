using System;
using FluentAssertions;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Serialization;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class BlockTests
{
    [Test]
    public void TouchTrigger_MultiTrigger_ShouldReturnTrueAfterSet()
    {
        var trigger = new TouchTrigger();
        var descriptor = new TypeDescriptor<TouchTrigger>();

        trigger.MultiTrigger = true;

        trigger.MultiTrigger.Should().Be(true);

        var raw = descriptor.AsString(trigger);
        raw.Should().NotContain("87");
    }

    [Test]
    public void TouchTrigger_MultiTrigger_ShouldReturnFalseAfterSet()
    {
        var trigger = new TouchTrigger();
        var descriptor = new TypeDescriptor<TouchTrigger>();

        trigger.MultiTrigger = false;

        trigger.MultiTrigger.Should().Be(false);

        var raw = descriptor.AsString(trigger);
        raw.Should().Contain("87,1");
    }

    [Test]
    public void TouchTrigger_MultiTrigger_ShouldTrueIfNotSet()
    {
        var raw = "1,999,2,0,3,0";
        var descriptor = new TypeDescriptor<TouchTrigger>();

        var trigger = descriptor.Create(raw.AsSpan());

        trigger.MultiTrigger.Should().BeTrue();
    }

    [Test]
    public void TouchTrigger_MultiTrigger_ShouldTrueIfSetToZero()
    {
        var raw = "1,999,2,0,3,0,87,0";
        var descriptor = new TypeDescriptor<TouchTrigger>();

        var trigger = descriptor.Create(raw.AsSpan());

        trigger.MultiTrigger.Should().BeTrue();
    }

    [Test]
    public void TouchTrigger_MultiTrigger_ShouldFalseIfSet()
    {
        var raw = "1,999,2,0,3,0,87,1";
        var descriptor = new TypeDescriptor<TouchTrigger>();

        var trigger = descriptor.Create(raw.AsSpan());

        trigger.MultiTrigger.Should().BeFalse();
    }
}
