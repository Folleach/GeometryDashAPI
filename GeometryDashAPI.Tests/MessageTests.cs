using System.Linq;
using FluentAssertions;
using GeometryDashAPI.Server.Dtos;
using NUnit.Framework;

namespace GeometryDashAPI.Tests;

[TestFixture]
public class MessageTests
{
    [Test]
    public void DecryptMessage()
    {
        var message = Message.FromRaw("SGVsbG8sIGl0J3MgbGlicmFyeSB0ZXN0IHRpbWU=", "eBNEUBFbQUFBEUddV0IRX1FGQl5DXxJFUFJfV0FCHRRWWl8WQBJeVFREEkxeRBRfXF9V");

        message.Should().BeEquivalentTo(new Message()
        {
            Subject = "Hello, it's library test time",
            Body = "I've just view network packets, don't keep you mind"
        });
    }

    [Test]
    public void EncryptMessage()
    {
        var message = new Message()
        {
            Subject = "Hello, it's library test time",
            Body = "I've just view network packets, don't keep you mind"
        };

        var parameters = message.BuildQuery();

        parameters.FirstOrDefault(x => x.Key == "subject").Value.Should().BeEquivalentTo("SGVsbG8sIGl0J3MgbGlicmFyeSB0ZXN0IHRpbWU=");
        parameters.FirstOrDefault(x => x.Key == "body").Value.Should().BeEquivalentTo("eBNEUBFbQUFBEUddV0IRX1FGQl5DXxJFUFJfV0FCHRRWWl8WQBJeVFREEkxeRBRfXF9V");
    }
}
