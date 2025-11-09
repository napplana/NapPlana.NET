using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Meta;

public class MetaEventBase: OneBotEvent
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Meta;
    
    [JsonPropertyName("meta_event_type")]
    public virtual MetaEventType MetaEventType { get; set; }
    
}