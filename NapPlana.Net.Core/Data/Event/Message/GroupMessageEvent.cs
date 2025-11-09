using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

public class GroupSender
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("card")]
    public string Card { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public GroupRole Role { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
}

public class GroupMessageEvent : MessageEventBase
{
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; } = MessageType.Group;

    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }

    [JsonPropertyName("anonymous")]
    public object? Anonymous { get; set; }

    [JsonPropertyName("sender")]
    public GroupSender Sender { get; set; } = new();
}
