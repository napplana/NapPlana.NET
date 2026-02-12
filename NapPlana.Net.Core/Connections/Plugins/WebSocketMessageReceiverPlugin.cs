using System.Text.Json;
using NapPlana.Core.Data.Action;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Event.Parser;
using TouchSocket.Core;
using TouchSocket.Http;
using TouchSocket.Http.WebSockets;
using LogLevel = NapPlana.Core.Data.LogLevel;
using NapPlana.Core.API;

namespace NapPlana.Core.Connections.Plugins;

/// <summary>
/// TouchSocket WebSocket消息接收插件
/// </summary>
public class WebSocketMessageReceiverPlugin: PluginBase, IWebSocketReceivedPlugin
{
    private readonly RootEventParser _parser = new();

    /// <summary>
    /// 当接收到WebSocket消息时触发
    /// </summary>
    /// <param name="webSocket">ws接口</param>
    /// <param name="e">参数</param>
    /// <returns></returns>
    public Task OnWebSocketReceived(IWebSocket webSocket, WSDataFrameEventArgs e)
    {
        var text = e.DataFrame.ToText();
        //处理接收到的消息
        BotEventHandler.LogReceived(LogLevel.Debug,$"接收到消息: {text}");
        if (!string.IsNullOrWhiteSpace(text))
        {
            try
            {
                //检查是否包含retcode字段,status会撞车
                using var doc = JsonDocument.Parse(text);
                if (doc.RootElement.TryGetProperty("retcode", out _))
                {
                    var actionResponse = JsonSerializer.Deserialize<ActionResponse>(text);
                    if (actionResponse != null)
                    {
                        if (actionResponse.RetCode != 0)
                        {
                            BotEventHandler.LogReceived(LogLevel.Error,$"动作失败: {actionResponse.RetCode} - {actionResponse.Message}");
                        }
                        // 将动作响应加入可消费队列
                        BotApiHandler.Dispatch(actionResponse);
                        // 不向事件解析器传递动作响应文本
                        return EasyTask.CompletedTask;
                    }
                }
            }
            catch (Exception ex)
            {
                BotEventHandler.LogReceived(LogLevel.Debug,$"解析动作响应时出现异常(忽略): {ex.Message}");
            }
        }
        //将消息传递给事件解析器
        _parser.ParseEvent(text?? "");
        return EasyTask.CompletedTask;
    }
}

/// <summary>
/// TouchSocket WebSocket认证插件
/// </summary>
/// <param name="token">令牌，没有可不传</param>
public class WebSocketAuthPlugin(string? token) : PluginBase, IWebSocketConnectingPlugin
{
    public Task OnWebSocketConnecting(IWebSocket webSocket, HttpContextEventArgs e)
    {
        if (!string.IsNullOrEmpty(token))
        {
            e.Context.Request.Headers.Add("Authorization", $"Bearer {token}");
        }
        return e.InvokeNext();
    }
}