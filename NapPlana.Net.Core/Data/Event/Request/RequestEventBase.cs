using System.Text.Json.Serialization;
using NapPlana.Core.Data.Event.Notice;

namespace NapPlana.Core.Data.Event.Request;

public class FriendRequestEvent: NoticeEventBase
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Request;
    
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.Friend;
    
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;
}

public class GroupRequestEvent: NoticeEventBase
{
    [JsonPropertyName("post_type")]
    public override EventType PostType { get; set; } = EventType.Request;
    
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.Group;
    
    [JsonPropertyName("sub_type")]
    public string SubType { get; set; } = "";
    
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;
    
    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;
}