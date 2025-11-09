using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

public class PokeNoticeEvent : NoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;

    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.Poke;

    [JsonPropertyName("target_id")]
    public long TargetId { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}

public class FriendPokeNoticeEvent : PokeNoticeEvent
{
    [JsonPropertyName("raw_info")]
    public object? RawInfo { get; set; }

    [JsonPropertyName("sender_id")]
    public long SenderId { get; set; }
}

public class GroupPokeNoticeEvent : PokeNoticeEvent
{
    [JsonPropertyName("group_id")]
    public long GroupId { get; set; }

    [JsonPropertyName("raw_info")]
    public object? RawInfo { get; set; }
}

public class ProfileLikeNoticeEvent : NoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;

    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.ProfileLike;

    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }

    [JsonPropertyName("operator_nick")]
    public string OperatorNick { get; set; } = string.Empty;

    [JsonPropertyName("times")]
    public int Times { get; set; }
    
}

public class InputStatusNoticeEvent : NoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;

    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.InputStatus;

    [JsonPropertyName("status_text")]
    public string StatusText { get; set; } = string.Empty;

    [JsonPropertyName("event_type")]
    public int InputEventType { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("group_id")]
    public long? GroupId { get; set; }
}

public class BotOfflineEvent : NoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.BotOffline;

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
