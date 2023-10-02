using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos;

[Sense(":")]
public class MessageContent : MessagePreview
{
    [GameProperty("5")]
    private string body;

    public string Body
    {
        get => Message.DeserializeBody(body);
        set => Message.SerializeBody(value);
    }

    public Message Message => new()
    {
        Body = Message.DeserializeBody(body),
        Subject = Subject
    };
}
