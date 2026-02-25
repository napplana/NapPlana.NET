using NapPlana.Core.Data.API;

namespace NapPlana.Core.Bot.BotInstance;

public interface INapBot
{
    /// <summary>
    /// QQ号等一系列能够唯一标识一个机器人的数字
    /// <remarks>
    /// 如果后期对接官方机器人，则需要改成字符串以保证兼容性，现在不需要
    /// </remarks>
    /// </summary>
    long SelfId { get; set; }
    /// <summary>
    /// 发送群聊消息
    /// </summary>
    /// <param name="groupMessage">消息结构</param>
    /// <param name="timeoutSeconds">超时时间</param>
    /// <returns>响应数据</returns>
    Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage,
        int timeoutSeconds = 15);
    /// <summary>
    /// 发送私聊消息
    /// </summary>
    /// <param name="privateMessage">消息结构</param>
    /// <param name="timeoutSeconds">超时时间</param>
    /// <returns>响应数据</returns>
    Task<PrivateMessageSendResponseData> SendPrivateMessageAsync(PrivateMessageSend privateMessage,
        int timeoutSeconds = 15);

    /// <summary>
    /// 发送戳一戳
    /// </summary>
    /// <param name="pokeMessage">消息结构</param>
    /// <returns>无</returns>
    Task SendPokeAsync(PokeMessageSend pokeMessage);
    /// <summary>
    /// 撤回消息
    /// </summary>
    /// <param name="deleteGroupMessage">消息结构</param>
    /// <returns>无</returns>
    Task DeleteGroupMessageAsync(GroupMessageDelete deleteGroupMessage);
    
    /// <summary>
    /// 发送群合并转发消息
    /// </summary>
    Task<ForwardMessageSendResponseData> SendGroupForwardMessageAsync(GroupForwardMessageSend message, int timeoutSeconds = 15);
    
    /// <summary>
    /// 发送私聊合并转发消息
    /// </summary>
    Task<ForwardMessageSendResponseData> SendPrivateForwardMessageAsync(PrivateForwardMessageSend message, int timeoutSeconds = 15);
    
    /// <summary>
    /// 贴表情
    /// </summary>
    /// <param name="message">消息结构</param>
    Task SetMsgEmojiLikeAsync(MsgEmojiLikeSend message);
    /// <summary>
    /// 获取文件信息
    /// </summary>
    Task<GetFileResponseData> GetFileAsync(GetFileRequest request, int timeoutSeconds = 15);
    
    /// <summary>
    /// 获取群文件下载链接
    /// </summary>
    Task<GetFileUrlResponseData> GetGroupFileUrlAsync(GetGroupFileUrlRequest request, int timeoutSeconds = 15);
    
    /// <summary>
    /// 获取私聊文件下载链接
    /// </summary>
    Task<GetFileUrlResponseData> GetPrivateFileUrlAsync(GetPrivateFileUrlRequest request, int timeoutSeconds = 15);
    
    /// <summary>
    /// 处理好友添加请求
    /// </summary>
    /// <param name="request">请求结构</param>
    /// <returns>无</returns>
    Task SetFriendAddRequestAsync(FriendAddRequestAction request);
    
    /// <summary>
    /// 处理群添加请求
    /// </summary>
    /// <param name="request">请求结构</param>
    /// <returns>无</returns>
    Task SetGroupAddRequestAsync(GroupAddRequestAction request);
}