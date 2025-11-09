using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

public class NoticeEventBase: OneBotEvent
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Notice;
    
    [JsonPropertyName("notice_type")]
    public virtual NoticeType NoticeType { get; set; }
}