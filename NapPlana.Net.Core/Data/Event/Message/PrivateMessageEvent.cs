using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

public class PrivateSender
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("sex")]
    public SexType Sex { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }
}

public class PrivateMessageEvent : MessageEventBase
{
    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; } = MessageType.Private;

    [JsonPropertyName("sub_type")]
    public PrivateMessageSubType SubType { get; set; }

    [JsonPropertyName("sender")]
    public PrivateSender Sender { get; set; } = new();
}
