using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

public class FriendAddNoticeEvent: NoticeEventBase
{
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; }= NoticeType.FriendAdd;
    
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
}

public class FriendRecallNoticeEvent: NoticeEventBase
{
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; }= NoticeType.FriendRecall;
    
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
}