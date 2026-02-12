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
    /// 启动机器人实例
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task StartAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// 关闭机器人
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task StopAsync(CancellationToken cancellationToken = default);
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
}