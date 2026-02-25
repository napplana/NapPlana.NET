using System.Text.Json.Serialization;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data;

/// <summary>
/// 日志级别。
/// </summary>
public enum LogLevel
{
    /// <summary>
    /// 无。
    /// </summary>
    None = 0,
    /// <summary>
    /// 错误。
    /// </summary>
    Error = 1,
    /// <summary>
    /// 警告。
    /// </summary>
    Warning = 2,
    /// <summary>
    /// 信息。
    /// </summary>
    Info = 3,
    /// <summary>
    /// 调试。
    /// </summary>
    Debug = 4
}

/// <summary>
/// 生命周期子类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum LifeCycleSubType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 启用。
    /// </summary>
    [JsonPropertyName("enable")]
    Enable,
    /// <summary>
    /// 禁用。
    /// </summary>
    [JsonPropertyName("disable")]
    Disable,
    /// <summary>
    /// 连接。
    /// </summary>
    [JsonPropertyName("connect")]
    Connect
}

/// <summary>
/// 群成员增加类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupIncreaseType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 批准。
    /// </summary>
    [JsonPropertyName("approve")]
    Approve,
    /// <summary>
    /// 邀请。
    /// </summary>
    [JsonPropertyName("invite")]
    Invite
}

/// <summary>
/// 群成员减少类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupDecreaseType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 踢出。
    /// </summary>
    [JsonPropertyName("kick")]
    Kick,
    /// <summary>
    /// 离开。
    /// </summary>
    [JsonPropertyName("leave")]
    Leave,
    /// <summary>
    /// 踢出我��
    /// </summary>
    [JsonPropertyName("kick_me")]
    KickMe,
    /// <summary>
    /// 解散。
    /// </summary>
    [JsonPropertyName("disband")]
    Disband
}

/// <summary>
/// 群管理员类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupManagerType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 设置。
    /// </summary>
    [JsonPropertyName("set")]
    Set,
    /// <summary>
    /// 取消设置。
    /// </summary>
    [JsonPropertyName("unset")]
    Unset
}

/// <summary>
/// 群禁言类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupBanType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 禁言。
    /// </summary>
    [JsonPropertyName("ban")]
    Ban,
    /// <summary>
    /// 解除禁言。
    /// </summary>
    [JsonPropertyName("lift_ban")]
    LiftBan
}

/// <summary>
/// 群精华消息类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupEssenceType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 添加。
    /// </summary>
    [JsonPropertyName("add")]
    Add,
    /// <summary>
    /// 删除。
    /// </summary>
    [JsonPropertyName("delete")]
    Delete
}

/// <summary>
/// 消息类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum MessageType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 私聊。
    /// </summary>
    [JsonPropertyName("private")]
    Private,
    /// <summary>
    /// 群聊。
    /// </summary>
    [JsonPropertyName("group")]
    Group
}

/// <summary>
/// 私信消息子类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum PrivateMessageSubType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 好友。
    /// </summary>
    [JsonPropertyName("friend")]
    Friend,
    /// <summary>
    /// 群聊。
    /// </summary>
    [JsonPropertyName("group")]
    Group,
    /// <summary>
    /// 其他。
    /// </summary>
    [JsonPropertyName("other")]
    Other
}

/// <summary>
/// 性别类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum SexType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 男。
    /// </summary>
    [JsonPropertyName("male")]
    Male,
    /// <summary>
    /// 女。
    /// </summary>
    [JsonPropertyName("female")]
    Female,
    /// <summary>
    /// 未知。
    /// </summary>
    [JsonPropertyName("unknown")]
    Unknown
}

/// <summary>
/// 群角色。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum GroupRole
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 群主。
    /// </summary>
    [JsonPropertyName("owner")]
    Owner,
    /// <summary>
    /// 管理员。
    /// </summary>
    [JsonPropertyName("admin")]
    Admin,
    /// <summary>
    /// 成员。
    /// </summary>
    [JsonPropertyName("member")]
    Member
}

/// <summary>
/// 元事件类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum MetaEventType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None = -1,
    /// <summary>
    /// 心跳。
    /// </summary>
    [JsonPropertyName("heartbeat")]
    Heartbeat = 0,
    /// <summary>
    /// 生命周期。
    /// </summary>
    [JsonPropertyName("lifecycle")]
    Lifecycle = 1
}

/// <summary>
/// 通知类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum NoticeType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    
    // request-related
    /// <summary>
    /// 好友。
    /// </summary>
    [JsonPropertyName("friend")]
    Friend,
    /// <summary>
    /// 群。
    /// </summary>
    [JsonPropertyName("group")]
    Group,
    
    // friend notices
    /// <summary>
    /// 好友添加。
    /// </summary>
    [JsonPropertyName("friend_add")]
    FriendAdd,
    /// <summary>
    /// 好友撤回。
    /// </summary>
    [JsonPropertyName("friend_recall")]
    FriendRecall,
    
    // group notices
    /// <summary>
    /// 群撤回。
    /// </summary>
    [JsonPropertyName("group_recall")]
    GroupRecall,
    /// <summary>
    /// 群增加。
    /// </summary>
    [JsonPropertyName("group_increase")]
    GroupIncrease,
    /// <summary>
    /// 群减少。
    /// </summary>
    [JsonPropertyName("group_decrease")]
    GroupDecrease,
    /// <summary>
    /// 群管理员。
    /// </summary>
    [JsonPropertyName("group_admin")]
    GroupAdmin,
    /// <summary>
    /// 群禁言。
    /// </summary>
    [JsonPropertyName("group_ban")]
    GroupBan,
    /// <summary>
    /// 群上传。
    /// </summary>
    [JsonPropertyName("group_upload")]
    GroupUpload,
    /// <summary>
    /// 群名片。
    /// </summary>
    [JsonPropertyName("group_card")]
    GroupCard,
    /// <summary>
    /// 群消息表情点赞。
    /// </summary>
    [JsonPropertyName("group_msg_emoji_like")]
    GroupMsgEmojiLike,
    /// <summary>
    /// 精华。
    /// </summary>
    [JsonPropertyName("essence")]
    Essence,
    
    // notify wrapper for subtypes like poke, profile_like, input_status, title, group_name
    /// <summary>
    /// 通知。
    /// </summary>
    [JsonPropertyName("notify")]
    Notify,
    
    // other
    /// <summary>
    /// 机器人离线。
    /// </summary>
    [JsonPropertyName("bot_offline")]
    BotOffline
}

/// <summary>
/// 请求类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum RequestType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 好友请求。
    /// </summary>
    [JsonPropertyName("friend")]
    Friend,
    /// <summary>
    /// 群请求。
    /// </summary>
    [JsonPropertyName("group")]
    Group
}

/// <summary>
/// 通知子类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum NotifySubType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 戳一戳。
    /// </summary>
    [JsonPropertyName("poke")]
    Poke,
    /// <summary>
    /// 个人资料点赞。
    /// </summary>
    [JsonPropertyName("profile_like")]
    ProfileLike,
    /// <summary>
    /// 输入状态。
    /// </summary>
    [JsonPropertyName("input_status")]
    InputStatus,
    /// <summary>
    /// 头衔。
    /// </summary>
    [JsonPropertyName("title")]
    Title,
    /// <summary>
    /// 群名称。
    /// </summary>
    [JsonPropertyName("group_name")]
    GroupName
}

/// <summary>
/// 消息数据类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum MessageDataType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None,
    /// <summary>
    /// 文本。
    /// </summary>
    [JsonPropertyName("text")]
    Text,
    /// <summary>
    /// 图片。
    /// </summary>
    [JsonPropertyName("image")]
    Image,
    /// <summary>
    /// 表情。
    /// </summary>
    [JsonPropertyName("face")]
    Face,
    /// <summary>
    /// @。
    /// </summary>
    [JsonPropertyName("at")]
    At,
    /// <summary>
    /// 音频。
    /// </summary>
    [JsonPropertyName("audio")]
    Audio,
    /// <summary>
    /// 录音。
    /// </summary>
    [JsonPropertyName("record")]
    Record,
    /// <summary>
    /// 视频。
    /// </summary>
    [JsonPropertyName("video")]
    Video,
    /// <summary>
    /// 石头剪刀布。
    /// </summary>
    [JsonPropertyName("rps")]
    Rps,
    /// <summary>
    /// 联系人。
    /// </summary>
    [JsonPropertyName("contact")]
    Contact,
    /// <summary>
    /// 骰子。
    /// </summary>
    [JsonPropertyName("dice")]
    Dice,
    /// <summary>
    /// 音乐。
    /// </summary>
    [JsonPropertyName("music")]
    Music,
    /// <summary>
    /// 回复。
    /// </summary>
    [JsonPropertyName("reply")]
    Reply,
    /// <summary>
    /// 转发。
    /// </summary>
    [JsonPropertyName("forward")]
    Forward,
    /// <summary>
    /// 节点。
    /// </summary>
    [JsonPropertyName("node")]
    Node,
    /// <summary>
    /// JSON。
    /// </summary>
    [JsonPropertyName("json")]
    Json,
    /// <summary>
    /// MFace。
    /// </summary>
    [JsonPropertyName("mface")]
    MFace,
    /// <summary>
    /// 文件。
    /// </summary>
    [JsonPropertyName("file")]
    File
}

/// <summary>
/// API动作类型。
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum ApiActionType
{
    /// <summary>
    /// 无。
    /// </summary>
    [JsonPropertyName("none")]
    None = -1,
    /// <summary>
    /// 发送私聊消息。
    /// </summary>
    [JsonPropertyName("send_private_msg")]
    SendPrivateMsg = 0,
    /// <summary>
    /// 发送群消息。
    /// </summary>
    [JsonPropertyName("send_group_msg")]
    SendGroupMsg = 1,
    /// <summary>
    /// 发送消息。
    /// </summary>
    [JsonPropertyName("send_msg")]
    SendMsg = 2,

    /// <summary>
    /// 删除消息。
    /// </summary>
    [JsonPropertyName("delete_msg")]
    DeleteMsg = 3,
    /// <summary>
    /// 获取消息。
    /// </summary>
    [JsonPropertyName("get_msg")]
    GetMsg = 4,
    /// <summary>
    /// 获取转发消息。
    /// </summary>
    [JsonPropertyName("get_forward_msg")]
    GetForwardMsg = 5,
    
    /// <summary>
    /// 发送点赞。
    /// </summary>
    [JsonPropertyName("send_like")]
    SendLike = 6,

    /// <summary>
    /// 设置群踢出。
    /// </summary>
    [JsonPropertyName("set_group_kick")]
    SetGroupKick = 7,
    /// <summary>
    /// 设置群禁言。
    /// </summary>
    [JsonPropertyName("set_group_ban")]
    SetGroupBan = 8,
    /// <summary>
    /// 设置群全员禁言。
    /// </summary>
    [JsonPropertyName("set_group_whole_ban")]
    SetGroupWholeBan = 9,
    /// <summary>
    /// 设置群管理员。
    /// </summary>
    [JsonPropertyName("set_group_admin")]
    SetGroupAdmin = 10,
    /// <summary>
    /// 设置群名片。
    /// </summary>
    [JsonPropertyName("set_group_card")]
    SetGroupCard = 11,
    /// <summary>
    /// 设置群名称。
    /// </summary>
    [JsonPropertyName("set_group_name")]
    SetGroupName = 12,
    /// <summary>
    /// 设置群离开。
    /// </summary>
    [JsonPropertyName("set_group_leave")]
    SetGroupLeave = 13,
    /// <summary>
    /// 设置群���殊头衔。
    /// </summary>
    [JsonPropertyName("set_group_special_title")]
    SetGroupSpecialTitle = 14,
    /// <summary>
    /// 设置好友添加请求。
    /// </summary>
    [JsonPropertyName("set_friend_add_request")]
    SetFriendAddRequest = 15,
    /// <summary>
    /// 设置群添加请求。
    /// </summary>
    [JsonPropertyName("set_group_add_request")]
    SetGroupAddRequest = 16,

    /// <summary>
    /// 获取登录信息。
    /// </summary>
    [JsonPropertyName("get_login_info")]
    GetLoginInfo = 17,
    /// <summary>
    /// 获取陌生人信息。
    /// </summary>
    [JsonPropertyName("get_stranger_info")]
    GetStrangerInfo = 18,
    /// <summary>
    /// 获取好���列表。
    /// </summary>
    [JsonPropertyName("get_friend_list")]
    GetFriendList = 19,
    /// <summary>
    /// 获取群信息。
    /// </summary>
    [JsonPropertyName("get_group_info")]
    GetGroupInfo = 20,
    /// <summary>
    /// 获取群列表。
    /// </summary>
    [JsonPropertyName("get_group_list")]
    GetGroupList = 21,
    /// <summary>
    /// 获取群成员信息。
    /// </summary>
    [JsonPropertyName("get_group_member_info")]
    GetGroupMemberInfo = 22,
    /// <summary>
    /// 获取群成员列表。
    /// </summary>
    [JsonPropertyName("get_group_member_list")]
    GetGroupMemberList = 23,
    /// <summary>
    /// 获取群荣誉信息。
    /// </summary>
    [JsonPropertyName("get_group_honor_info")]
    GetGroupHonorInfo = 24,
    /// <summary>
    /// 获取Cookies。
    /// </summary>
    [JsonPropertyName("get_cookies")]
    GetCookies = 25,
    /// <summary>
    /// 获取CSRF令牌。
    /// </summary>
    [JsonPropertyName("get_csrf_token")]
    GetCsrfToken = 26,
    /// <summary>
    /// 获取凭据。
    /// </summary>
    [JsonPropertyName("get_credentials")]
    GetCredentials = 27,
    /// <summary>
    /// 获取录音。
    /// </summary>
    [JsonPropertyName("get_record")]
    GetRecord = 28,
    /// <summary>
    /// 获取图片。
    /// </summary>
    [JsonPropertyName("get_image")]
    GetImage = 29,
    /// <summary>
    /// 能否发送图片。
    /// </summary>
    [JsonPropertyName("can_send_image")]
    CanSendImage = 30,
    /// <summary>
    /// 能否发送录音。
    /// </summary>
    [JsonPropertyName("can_send_record")]
    CanSendRecord = 31,
    /// <summary>
    /// 获取状态。
    /// </summary>
    [JsonPropertyName("get_status")]
    GetStatus = 32,
    /// <summary>
    /// 获取版本信息。
    /// </summary>
    [JsonPropertyName("get_version_info")]
    GetVersionInfo = 33,
    /// <summary>
    /// 清理缓存。
    /// </summary>
    [JsonPropertyName("clean_cache")]
    CleanCache = 34,
    
    /// <summary>
    /// 发送戳一戳。
    /// </summary>
    [JsonPropertyName("send_poke")]
    SendPoke = 35,
    
    /// <summary>
    /// 发送群合并转发消息。
    /// </summary>
    [JsonPropertyName("send_group_forward_msg")]
    SendGroupForwardMsg = 36,
    
    /// <summary>
    /// 发送私聊合并转发消息。
    /// </summary>
    [JsonPropertyName("send_private_forward_msg")]
    SendPrivateForwardMsg = 37,
    
    /// <summary>
    /// 发送合并转发消息。
    /// </summary>
    [JsonPropertyName("send_forward_msg")]
    SendForwardMsg = 38,

    /// <summary>
    /// 贴表情
    /// </summary>
    [JsonPropertyName("set_msg_emoji_like")]
    SetMsgEmojiLike = 39
    
    /// <summary>
    /// 获取文件信息。
    /// </summary>
    [JsonPropertyName("get_file")]
    GetFile = 40,
    
    /// <summary>
    /// 获取群文件下载链接。
    /// </summary>
    [JsonPropertyName("get_group_file_url")]
    GetGroupFileUrl = 41,
    
    /// <summary>
    /// 获取私聊文件下载链接。
    /// </summary>
    [JsonPropertyName("get_private_file_url")]
    GetPrivateFileUrl = 42
}

/// <summary>
/// 事件类型
/// </summary>
[JsonConverter(typeof(SafeJsonStringEnumConverter))]
public enum EventType
{
    /// <summary>
    /// 无
    /// </summary>
    [JsonPropertyName("none")]
    None = -1,
    /// <summary>
    /// 元事件
    /// </summary>
    [JsonPropertyName("meta_event")]
    Meta = 0,
    
    /// <summary>
    /// 请求
    /// </summary>
    [JsonPropertyName("request")]
    Request = 1,
    /// <summary>
    /// 通知
    /// </summary>
    [JsonPropertyName("notice")]
    Notice = 2,
    
    /// <summary>
    /// 消息
    /// </summary>
    [JsonPropertyName("message")]
    Message = 3,
    
    /// <summary>
    /// 自身发送消息
    /// </summary>
    [JsonPropertyName("message_sent")]
    MessageSent = 4
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