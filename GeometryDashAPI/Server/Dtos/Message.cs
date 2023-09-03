using System;
using System.Text;

namespace GeometryDashAPI.Server.Dtos;

public class Message : IQuery
{
    private static readonly string BodyXorKey = "14251";

    public string Subject { get; set; }
    public string Body { get; set; }

    public static Message FromRaw(string subject, string body)
    {
        return new Message()
        {
            Subject = DeserializeSubject(subject),
            Body = DeserializeBody(body)
        };
    }

    public Parameters BuildQuery()
    {
        var parameters = new Parameters();
        BuildQuery(parameters);
        return parameters;
    }

    public void BuildQuery(Parameters parameters)
    {
        parameters.Add(new Property("subject", SerializeSubject(Subject)));
        parameters.Add(new Property("body", SerializeBody(Body)));
    }

    internal static string DeserializeSubject(string raw) => Encoding.UTF8.GetString(Convert.FromBase64String(raw));
    internal static string SerializeSubject(string subject) => Convert.ToBase64String(Encoding.ASCII.GetBytes(subject));

    internal static string DeserializeBody(string raw) => Crypt.XOR(Encoding.UTF8.GetString(Convert.FromBase64String(raw)), BodyXorKey);
    internal static string SerializeBody(string body) => Convert.ToBase64String(Encoding.ASCII.GetBytes(Crypt.XOR(body, BodyXorKey)));
}
