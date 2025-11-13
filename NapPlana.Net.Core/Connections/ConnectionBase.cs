using NapPlana.Core.API;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Action;

namespace NapPlana.Core.Connections;

/// <summary>
/// 基本连接类
/// </summary>
public class ConnectionBase: IConnectionBase
{
    protected string Ip = "";
    protected int Port;
    protected string? Token;
    public BotConnectionType ConnectionType { get; set; } = BotConnectionType.None;
    
    /// <summary>
    /// 初始化客户端/服务器连接
    /// </summary>
    public virtual async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }
    
    /// <summary>
    /// 关闭连接
    /// </summary>
    public virtual async Task ShutdownAsync()
    {
        await Task.CompletedTask;
    }
    
    /// <summary>
    /// 接收到消息时调用
    /// </summary>
    /// <param name="message"></param>
    public virtual async Task SendMessageAsync(string message)
    {
        await Task.CompletedTask;
    }
    
    /// <summary>
    /// 带参发送消息时调用
    /// </summary>
    /// <param name="actionType">操作类型</param>
    /// <param name="message">消息内容</param>
    /// <param name="echo">标识</param>
    public virtual async Task SendMessageAsync(ApiActionType actionType,object message,string echo)
    {
        await Task.CompletedTask;
    }
    
    /// <summary>
    /// 发送消息时调用
    /// </summary>
    public virtual async Task ReceiveMessageAsync()
    {
        await Task.CompletedTask;
    }
}