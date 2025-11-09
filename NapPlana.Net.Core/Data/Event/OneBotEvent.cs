using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event;

public class OneBotEvent
{
    [JsonPropertyName("time")]
    public long TimeStamp { get; set; }
    
    [JsonPropertyName("self_id")]
    public long SelfId { get; set; }
    
    [JsonPropertyName("post_type")]
    public virtual EventType PostType { get; set; } = EventType.None;
}