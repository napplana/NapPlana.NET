using System.Text.Json.Serialization;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.API;

/// <summary>
/// 发送群组消息
/// </summary>
public class GroupMessageSend
{
    /// <summary>
    /// 群号
    /// </summary>
    [JsonPropertyName("group_id")]
    public string GroupId { get; set; } = string.Empty;
    
    /// <summary>
    /// 消息链
    /// </summary>
    [JsonPropertyName("message")]
    [JsonConverter(typeof(MessageListConverter))]
    public List<MessageBase> Message { get; set; } = new();
}

/// <summary>
/// 发送群组消息-响应
/// </summary>
public class GroupMessageSendResponseData : ResponseDataBase
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; } = 0;
}

/// <summary>
/// 发送戳一戳
/// </summary>
public class PokeMessageSend
{
    /// <summary>
    /// 群号，可不填，不填则为私聊
    /// </summary>
    [JsonPropertyName("group_id")]
    public string? GroupId { get; set; } = null;
    
    /// <summary>
    /// 必填，用户ID
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;
    
    /// <summary>
    /// 似乎也是用户id
    /// </summary>
    [JsonPropertyName("target_id")]
    public string TargetId { get; set; } = string.Empty;
}

/// <summary>
/// 撤回消息
/// </summary>
public class GroupMessageDelete
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; } = 0;
}