using NapPlana.Core.Bot.BotInstance;
using NapPlana.Core.Data.API;
using NapPlana.Core.Data.Action;
using NapPlana.Core.API;
using NapPlana.Core.Connections;
using NapPlana.Core.Event.Handler;
using Microsoft.Extensions.Options;
using NapPlana.Core.Data;
using NapPlana.Core.DependencyInjection;

namespace NapPlana.DI.Service;

/// <summary>
/// Bot上下文 - 提供API调用能力，不参与生命周期管理
/// </summary>
/// <remarks>
/// BotContext仅负责接口调用和事件回调，在连接建立后可被注入依赖，连接关闭后被dispose
/// </remarks>
public class BotContext : INapBot, IDisposable
{
    private readonly IApiHandler _apiHandler;
    public readonly IEventHandler EventHandler;
    private readonly ConnectionBase _connection;
    private readonly NapBotOptions _options;
    private bool _disposed;

    /// <summary>
    /// 机器人QQ号
    /// </summary>
    public long SelfId { get; set; }

    /// <summary>
    /// 构造函数 - 通过依赖注入获取核心服务
    /// </summary>
    /// <param name="apiHandler">API处理器</param>
    /// <param name="eventHandler">事件处理器</param>
    /// <param name="connection">连接实例</param>
    /// <param name="options">配置选项</param>
    public BotContext(
        IApiHandler apiHandler,
        IEventHandler eventHandler,
        ConnectionBase connection,
        IOptions<NapBotOptions> options)
    {
        _apiHandler = apiHandler ?? throw new ArgumentNullException(nameof(apiHandler));
        EventHandler = eventHandler ?? throw new ArgumentNullException(nameof(eventHandler));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        
        SelfId = _options.SelfId;
    }

    /// <summary>
    /// 发送消息的统一处理方法
    /// </summary>
    private async Task<T?> SendMessageAsync<T>(object message, ApiActionType actionType, int timeoutSeconds = 15) where T : ResponseDataBase
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(BotContext));

        if (message is null)
            throw new ArgumentNullException(nameof(message));

        var echo = Guid.NewGuid().ToString();
        var timeout = TimeSpan.FromSeconds(timeoutSeconds);
        var cts = new CancellationTokenSource(timeout);

        var tcs = new TaskCompletionSource<ActionResponse>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!_apiHandler.TryRegister(echo, tcs))
        {
            throw new InvalidOperationException("Echo register failed.");
        }

        // 超时处理
        cts.Token.Register(() =>
        {
            if (_apiHandler.TryRemove(echo, out var pending))
            {
                pending?.TrySetException(new TimeoutException($"Timed out waiting for {typeof(T)} response."));
            }
        });

        try
        {
            await _connection.SendMessageAsync(actionType, message, echo).ConfigureAwait(false);
            var baseResult = await tcs.Task.ConfigureAwait(false);
            var data = baseResult.GetData<T>();
            return data;
        }
        catch
        {
            _apiHandler.TryRemove(echo, out _);
            throw;
        }
        finally
        {
            cts.Dispose();
        }
    }

    /// <summary>
    /// 发送群聊消息
    /// </summary>
    public async Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage, int timeoutSeconds = 15)
    {
        if (groupMessage is null)
            throw new ArgumentNullException(nameof(groupMessage));

        var res = await SendMessageAsync<GroupMessageSendResponseData>(groupMessage, ApiActionType.SendGroupMsg, timeoutSeconds);
        return res ?? throw new Exception("Failed to send group message.");
    }

    /// <summary>
    /// 发送私聊消息
    /// </summary>
    public async Task<PrivateMessageSendResponseData> SendPrivateMessageAsync(PrivateMessageSend privateMessage, int timeoutSeconds = 15)
    {
        if (privateMessage is null)
            throw new ArgumentNullException(nameof(privateMessage));

        var res = await SendMessageAsync<PrivateMessageSendResponseData>(privateMessage, ApiActionType.SendPrivateMsg, timeoutSeconds);
        return res ?? throw new Exception("Failed to send private message.");
    }

    /// <summary>
    /// 发送戳一戳
    /// </summary>
    public async Task SendPokeAsync(PokeMessageSend pokeMessage)
    {
        if (pokeMessage is null)
            throw new ArgumentNullException(nameof(pokeMessage));

        await SendMessageAsync<ResponseDataBase>(pokeMessage, ApiActionType.SendPoke);
    }

    /// <summary>
    /// 撤回消息
    /// </summary>
    public async Task DeleteGroupMessageAsync(GroupMessageDelete deleteGroupMessage)
    {
        if (deleteGroupMessage is null)
            throw new ArgumentNullException(nameof(deleteGroupMessage));

        await SendMessageAsync<ResponseDataBase>(deleteGroupMessage, ApiActionType.DeleteMsg);
    }

    /// <summary>
    /// 贴表情
    /// </summary>
    public async Task SetMsgEmojiLikeAsync(MsgEmojiLikeSend message)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        await SendMessageAsync<ResponseDataBase>(message, ApiActionType.SetMsgEmojiLike);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        EventHandler.LogReceived(Core.Data.LogLevel.Debug, "BotContext 已释放");
    }
}