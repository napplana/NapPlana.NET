using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Message;

public class MessageSentEvent : MessageEventBase
{
    [JsonPropertyName("post_type")] 
    public override EventType PostType { get; set; } = EventType.MessageSent;

    [JsonPropertyName("message_type")]
    public MessageType MessageType { get; set; }

    [JsonPropertyName("target_id")]
    public long TargetId { get; set; }
}
