using NapPlana.Core.API;
using NapPlana.Core.Connections;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Action;
using NapPlana.Core.Data.API;

namespace NapPlana.Core.Bot;

/// <summary>
/// 机器人主体
/// </summary>
public class NapBot
{
    private ConnectionBase _connection;
    /// <summary>
    /// QQ号
    /// </summary>
    public  long SelfId = 0;
    
    /// <summary>
    /// 创建实例
    /// </summary>
    public NapBot()
    {
        // Default to a dummy connection; should be set properly later
        _connection = new ConnectionBase();
    }

    /// <summary>
    /// 带参创建
    /// </summary>
    /// <param name="connection">连接类型</param>
    /// <param name="selfId">QQ号</param>
    public NapBot(ConnectionBase connection,long selfId)
    {
        _connection = connection;
        SelfId = selfId;
    }

    /// <summary>
    /// 设置连接类型
    /// </summary>
    /// <param name="connection">连接类型</param>
    /// <returns>自身</returns>
    public NapBot SetConnection(ConnectionBase connection)
    {
        _connection = connection;
        return this;
    }

    /// <summary>
    /// 异步启动机器人
    /// </summary>
    /// <returns></returns>
    public Task StartAsync() => _connection.InitializeAsync();
    /// <summary>
    /// 异步终止机器人
    /// </summary>
    /// <returns></returns>
    public Task StopAsync() => _connection.ShutdownAsync();
    
    /// <summary>
    /// 发送消息的统一处理方法
    /// </summary>
    /// <param name="message">信息实体</param>
    /// <param name="actionType">动作类型</param>
    /// <param name="timer">自定义超时时间</param>
    /// <typeparam name="T">继承自ResponseDataBase的泛型，没有返回值的方法传入基类即可</typeparam>
    /// <returns>任务</returns>
    /// <exception cref="InvalidOperationException">远程响应错误</exception>
    /// <exception cref="TimeoutException">访问超时</exception>
    private async Task<T?> SendMessageAsync<T>(object message, ApiActionType actionType,int timer = 15) where T : ResponseDataBase
    {
        if (message is null) throw new ArgumentNullException(nameof(message));

        var echo = Guid.NewGuid().ToString();
        var timeout = TimeSpan.FromSeconds(timer);
        var cts = new CancellationTokenSource(timeout);

        var tcs = new TaskCompletionSource<ActionResponse>(TaskCreationOptions.RunContinuationsAsynchronously);

        if (!ApiHandler.TryRegister(echo, tcs))
            throw new InvalidOperationException("Echo register failed.");

        //超时
        cts.Token.Register(() =>
        {
            if (ApiHandler.TryRemove(echo, out var pending))
                pending?.TrySetException(new TimeoutException($"Timed out waiting for {typeof(T)} response."));
        });

        await _connection.SendMessageAsync(actionType, message, echo).ConfigureAwait(false);

        var baseResult = await tcs.Task.ConfigureAwait(false);
        //获取数据
        var data = baseResult.GetData<T>();
        return data;
    }
    
    /// <summary>
    /// 发送群消息
    /// </summary>
    /// <param name="groupMessage">请求</param>
    /// <param name="timeoutSeconds">自定义超时时间</param>
    /// <returns>响应</returns>
    /// <exception cref="ArgumentNullException">传参错误</exception>
    public async Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage,int timeoutSeconds = 15)
    {
        if (groupMessage is null) throw new ArgumentNullException(nameof(groupMessage));
        var res =  await SendMessageAsync<GroupMessageSendResponseData>(groupMessage,ApiActionType.SendGroupMsg);
        return res ?? throw new Exception("Failed to send group message.");
    }

    /// <summary>
    /// 发送私聊消息
    /// </summary>
    /// <param name="privateMessage">请求</param>
    /// <param name="timeoutSeconds">自定义超时时间</param>
    /// <returns>响应</returns>
    /// <exception cref="ArgumentNullException">传参错误</exception>
    public async Task<PrivateMessageSendResponseData> SendPrivateMessageAsync(PrivateMessageSend privateMessage, int timeoutSeconds = 15) {
        if (privateMessage is null) throw new ArgumentNullException(nameof(privateMessage));
        var res = await SendMessageAsync<PrivateMessageSendResponseData>(privateMessage, ApiActionType.SendPrivateMsg);
        return res ?? throw new Exception("Failed to send private message.");
    }

    /// <summary>
    /// 发送戳一戳消息
    /// </summary>
    /// <param name="pokeMessage">信息结构</param>
    public async Task SendPokeAsync(PokeMessageSend pokeMessage)
    {
        if (pokeMessage is null) throw new ArgumentNullException(nameof(pokeMessage));
        await SendMessageAsync<ResponseDataBase>(pokeMessage,ApiActionType.SendPoke);
    }


    /// <summary>
    ///  撤回消息
    /// </summary>
    /// <param name="deleteGroupMessage"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task DeleteGroupMessageAsync(GroupMessageDelete deleteGroupMessage)
    {
        if (deleteGroupMessage is null) throw new ArgumentNullException(nameof(deleteGroupMessage));
        await SendMessageAsync<ResponseDataBase>(deleteGroupMessage,ApiActionType.DeleteMsg);
    }
}