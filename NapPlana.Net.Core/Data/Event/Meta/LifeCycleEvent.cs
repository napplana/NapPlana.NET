using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Meta;

public class LifeCycleEvent: MetaEventBase
{
    [JsonPropertyName("meta_event_type")]
    public override MetaEventType MetaEventType { get; set; }= MetaEventType.Lifecycle;
    
    [JsonPropertyName("sub_type")]
    public LifeCycleSubType SubType { get; set; }
}