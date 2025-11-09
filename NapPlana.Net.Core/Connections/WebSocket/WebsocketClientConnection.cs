using NapPlana.Core.Connections.Plugins;
using NapPlana.Core.Data;
using NapPlana.Core.Event.Handler;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;
using LogLevel = NapPlana.Core.Data.LogLevel;

namespace NapPlana.Core.Connections.WebSocket;

public class WebsocketClientConnection: ConnectionBase
{
    private WebSocketClient? _client;
    
    public WebsocketClientConnection()
    {
        ConnectionType = BotConnectionType.WebSocketClient;
    }
    
    public WebsocketClientConnection(string ip, int port, string? token = null)
        : this()
    {
        this.Ip = ip;
        this.Port = port;
        this.Token = token;
    }

    public override async Task InitializeAsync()
    {
        if (_client != null)
        {
            return;
        }
        _client = new WebSocketClient();
        await _client.SetupAsync(new TouchSocketConfig()
            .ConfigurePlugins(a=>
            {
                //配置请求头
                a.Add(new WebSocketAuthPlugin(Token));
                //消息接收
                a.Add<WebSocketMessageReceiverPlugin>();
            })
            .SetRemoteIPHost($"ws://{Ip}:{Port}"));
        //setup callbacks
        _client.Connected += (sender, e) =>
        {
            BotEventHandler.LogReceived(LogLevel.Info,"机器人连接至napcat...等待后续操作");
            return EasyTask.CompletedTask;
        };
        try
        {
            await _client.ConnectAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"连接失败: {ex.Message}");
            throw;
        }
    }

    public override async Task ShutdownAsync()
    {
        if (_client == null)
        {
            return;
        }
        try
        {
            await _client.CloseAsync("shutdown");
            BotEventHandler.LogReceived(LogLevel.Info, "WebSocket客户端已关闭");
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"关闭WebSocket失败: {ex.Message}");
        }
        finally
        {
            _client.Dispose();
            _client = null;
        }
    }

    public override async Task SendMessageAsync(string message)
    {
        if (_client == null)
        {
            BotEventHandler.LogReceived(LogLevel.Error, "无法发送消息，WebSocket未初始化");
            return;
        }
        try
        {
            await _client.SendAsync(message);
            BotEventHandler.LogReceived(LogLevel.Debug, $"已发送消息: {message}");
        }
        catch (Exception ex)
        {
            BotEventHandler.LogReceived(LogLevel.Error, $"发送消息失败: {ex.Message}");
        }
    }
}