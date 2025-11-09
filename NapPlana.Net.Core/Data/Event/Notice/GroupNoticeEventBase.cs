using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

public class GroupNoticeEventBase: NoticeEventBase
{
    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}