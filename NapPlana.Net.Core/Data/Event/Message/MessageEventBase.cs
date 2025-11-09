using System.Text.Json.Serialization;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.Event.Message;

public class MessageEventBase : OneBotEvent
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Message;

    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("message")]
    [JsonConverter(typeof(MessageConverter))]
    public object Message { get; set; } = null!;

    [JsonPropertyName("raw_message")]
    public string RawMessage { get; set; } = string.Empty;
}