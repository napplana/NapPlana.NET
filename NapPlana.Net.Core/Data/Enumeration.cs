using System.Text.Json.Serialization;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data;

public enum LogLevel
{
    None = 0,
    Error = 1,
    Warning = 2,
    Info = 3,
    Debug = 4
}

/// <summary>
/// 机器人要如何与napcat连接?
/// </summary>
public enum BotConnectionType
{
    None = -1,
    /// <summary>
    /// 本机作为Http服务器,napcat作客户端
    /// </summary>
    HttpServer = 0,
    /// <summary>
    /// 本机作为WebSocket服务器,napcat作客户端
    /// </summary>
    WebSocketServer = 1,
    /// <summary>
    /// 本机作客户端,napcat作Http服务器
    /// </summary>
    HttpClient = 2,
    /// <summary>
    /// 本机作客户端,napcat作WebSocket服务器
    /// </summary>
    WebSocketClient = 3
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum EventType
{
    [JsonPropertyName("none")]
    None = -1,
    [JsonPropertyName("meta_event")]
    Meta = 0,
    
    [JsonPropertyName("request")]
    Request = 1,
    
    [JsonPropertyName("notice")]
    Notice = 2,
    
    [JsonPropertyName("message")]
    Message = 3,
    
    [JsonPropertyName("message_sent")]
    MessageSent = 4
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum LifeCycleSubType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("enable")]
    Enable,
    [JsonPropertyName("disable")]
    Disable,
    [JsonPropertyName("connect")]
    Connect
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupIncreaseType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("approve")]
    Approve,
    [JsonPropertyName("invite")]
    Invite
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupDecreaseType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("kick")]
    Kick,
    [JsonPropertyName("leave")]
    Leave,
    [JsonPropertyName("kick_me")]
    KickMe,
    [JsonPropertyName("disband")]
    Disband
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupManagerType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("set")]
    Set,
    [JsonPropertyName("unset")]
    Unset
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupBanType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("ban")]
    Ban,
    [JsonPropertyName("lift_ban")]
    LiftBan
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupEssenceType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("add")]
    Add,
    [JsonPropertyName("delete")]
    Delete
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum MessageType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("private")]
    Private,
    [JsonPropertyName("group")]
    Group
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum PrivateMessageSubType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("friend")]
    Friend,
    [JsonPropertyName("group")]
    Group,
    [JsonPropertyName("other")]
    Other
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum SexType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("male")]
    Male,
    [JsonPropertyName("female")]
    Female,
    [JsonPropertyName("unknown")]
    Unknown
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupRole
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("owner")]
    Owner,
    [JsonPropertyName("admin")]
    Admin,
    [JsonPropertyName("member")]
    Member
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum MetaEventType
{
    [JsonPropertyName("none")]
    None = -1,
    [JsonPropertyName("heartbeat")]
    Heartbeat = 0,
    [JsonPropertyName("lifecycle")]
    Lifecycle = 1
}

// New NoticeType enum extracted from all notice_type usages across the project
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum NoticeType
{
    [JsonPropertyName("none")]
    None,
    
    // request-related
    [JsonPropertyName("friend")]
    Friend,
    [JsonPropertyName("group")]
    Group,
    
    // friend notices
    [JsonPropertyName("friend_add")]
    FriendAdd,
    [JsonPropertyName("friend_recall")]
    FriendRecall,
    
    // group notices
    [JsonPropertyName("group_recall")]
    GroupRecall,
    [JsonPropertyName("group_increase")]
    GroupIncrease,
    [JsonPropertyName("group_decrease")]
    GroupDecrease,
    [JsonPropertyName("group_admin")]
    GroupAdmin,
    [JsonPropertyName("group_ban")]
    GroupBan,
    [JsonPropertyName("group_upload")]
    GroupUpload,
    [JsonPropertyName("group_card")]
    GroupCard,
    [JsonPropertyName("group_msg_emoji_like")]
    GroupMsgEmojiLike,
    [JsonPropertyName("essence")]
    Essence,
    
    // notify wrapper for subtypes like poke, profile_like, input_status, title, group_name
    [JsonPropertyName("notify")]
    Notify,
    
    // other
    [JsonPropertyName("bot_offline")]
    BotOffline
}

// Add enum for notify sub_type values
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum NotifySubType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("poke")]
    Poke,
    [JsonPropertyName("profile_like")]
    ProfileLike,
    [JsonPropertyName("input_status")]
    InputStatus,
    [JsonPropertyName("title")]
    Title,
    [JsonPropertyName("group_name")]
    GroupName
}

[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum MessageDataType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("text")]
    Text,
    [JsonPropertyName("image")]
    Image,
    [JsonPropertyName("face")]
    Face,
    [JsonPropertyName("at")]
    At,
    [JsonPropertyName("audio")]
    Audio,
    [JsonPropertyName("record")]
    Record,
    [JsonPropertyName("video")]
    Video,
    [JsonPropertyName("rps")]
    Rps,
    [JsonPropertyName("contact")]
    Contact,
    [JsonPropertyName("dice")]
    Dice,
    [JsonPropertyName("music")]
    Music,
    [JsonPropertyName("reply")]
    Reply,
    [JsonPropertyName("forward")]
    Forward,
    [JsonPropertyName("node")]
    Node,
    [JsonPropertyName("json")]
    Json,
    [JsonPropertyName("mface")]
    MFace,
    [JsonPropertyName("file")]
    File
}