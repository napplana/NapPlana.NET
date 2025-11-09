using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Meta;

public class HeartbeatStatus
{
    [JsonPropertyName("online")]
    public bool? Online { get; set; }  // 是否在线，可为 null

    [JsonPropertyName("good")]
    public bool Good { get; set; }     // 状态是否良好
}

public class HeartBeatEvent: MetaEventBase
{
    [JsonPropertyName("meta_event_type")]
    public override MetaEventType MetaEventType { get; set; }= MetaEventType.Heartbeat;
    
    [JsonPropertyName("status")]
    public HeartbeatStatus? Status { get; set; }
    
    [JsonPropertyName("interval")]
    public int Interval { get; set; }
}