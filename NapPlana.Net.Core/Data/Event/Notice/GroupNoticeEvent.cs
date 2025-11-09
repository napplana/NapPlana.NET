using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Event.Notice;

public class GroupRecallNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupRecall;

    [JsonPropertyName("message_id")] 
    public long MessageId { get; set; }
    
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
}

public class GroupIncreaseNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupIncrease;

    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    [JsonPropertyName("sub_type")]
    public GroupIncreaseType IncreaseType { get; set; }
}

public class GroupDecreaseNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupDecrease;

    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    [JsonPropertyName("sub_type")]
    public GroupDecreaseType DecreaseType { get; set; }
}

public class GroupAdminNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupAdmin;
    
    [JsonPropertyName("sub_type")]
    public GroupManagerType AdminType { get; set; }
}

/// <summary>
/// 这其实是禁言
/// </summary>
public class GroupBanNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupBan;
    
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
    
    [JsonPropertyName("sub_type")]
    public GroupBanType BanType { get; set; }
}

public class GroupUploadFileData
{
    [JsonPropertyName("id")] 
    public string FileId { get; set; } = "";
    
    [JsonPropertyName("busid")]
    public long BusId { get; set; }
    
    [JsonPropertyName("name")]
    public string FileName { get; set; } = "";
    
    [JsonPropertyName("size")]
    public long Size { get; set;  }
}

public class GroupUploadNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupUpload;
    
    [JsonPropertyName("file")]
    public GroupUploadFileData File { get; set; } = new GroupUploadFileData();
}

public class GroupCardEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupCard;
    
    [JsonPropertyName("card_old")]
    public string OldCard { get; set; } = "";
    
    [JsonPropertyName("card_new")]
    public string NewCard { get; set; } = "";
}

public class GroupNameEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;
    
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.GroupName;
    
    [JsonPropertyName("name_new")]
    public string NewName { get; set; } = "";
}

public class GroupTitleEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")] 
    public override NoticeType NoticeType { get; set; } = NoticeType.Notify;
    
    [JsonPropertyName("sub_type")]
    public NotifySubType SubType { get; set; } = NotifySubType.Title;
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}

public class GroupEssenceNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.Essence;
    
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }
    
    [JsonPropertyName("sender_id")]
    public long SenderId { get; set; }
    
    [JsonPropertyName("operator_id")]
    public long OperatorId { get; set; }
    
    [JsonPropertyName("sub_type")]
    public GroupEssenceType EssenceType { get; set; }
}

public class MsgEmojiLike
{
    [JsonPropertyName("emoji_id")]
    public string EmojiId { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }
}

public class GroupMsgEmojiLikeNoticeEvent : GroupNoticeEventBase
{
    [JsonPropertyName("notice_type")]
    public override NoticeType NoticeType { get; set; } = NoticeType.GroupMsgEmojiLike;

    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }

    [JsonPropertyName("likes")]
    public List<MsgEmojiLike> Likes { get; set; } = new();
}
