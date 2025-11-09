using NapPlana.Core.Data;

namespace NapPlana.Core.Connections;

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
    /// 发送消息时调用
    /// </summary>
    public virtual async Task ReceiveMessageAsync()
    {
        await Task.CompletedTask;
    }
}