using GeometryDashAPI.Attributes;

namespace GeometryDashAPI.Server.Dtos;

[Sense(":")]
public class MessagePreview : GameObject
{
    [GameProperty("1")]
    public int MessageId { get; set; }

    [GameProperty("2")]
    public int SenderAccountId { get; set; }

    [GameProperty("3")]
    public int SenderUserId { get; set; }

    [GameProperty("4")]
    private string subject;

    public string Subject
    {
        get => Message.DeserializeSubject(subject);
        set => Message.SerializeSubject(subject);
    }

    [GameProperty("6")]
    public string SenderUserName { get; set; }

    [GameProperty("7")]
    public string Time { get; set; }

    [GameProperty("8")]
    public bool HasBeenRead { get; set; }

    public override string ToString()
    {
        return $"Letter '{Subject}' from {SenderUserName}({SenderAccountId})";
    }
}
