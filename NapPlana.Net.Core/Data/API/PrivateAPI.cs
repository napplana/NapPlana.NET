using System.Text.Json.Serialization;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Utilities;

namespace NapPlana.Core.Data.API;

/// <summary>
/// 发送私聊消息
/// </summary>
public class PrivateMessageSend {
    /// <summary>
    /// 目标 QQ 号
    /// </summary>
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 消息链
    /// </summary>
    [JsonPropertyName("message")]
    [JsonConverter(typeof(MessageListConverter))]
    public List<MessageBase> Message { get; set; } = new();
}

/// <summary>
/// 发送私聊消息-响应
/// </summary>
public class PrivateMessageSendResponseData : ResponseDataBase {
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; } = 0;
}