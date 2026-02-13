using System.Text.Json;
using NapPlana.Core.API;
using NapPlana.Core.Data.Action;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Event.Parser;
using TouchSocket.Core;
using TouchSocket.Http.WebSockets;

namespace NapPlana.DI.Connection;

/// <summary>
/// 针对DI场景重建的WebSocket消息接收插件
/// </summary>
/// <param name="eventParser">事件解析器</param>
/// <param name="eventHandler">事件处理器</param>
/// <param name="apiHandler">请求处理器</param>
public class OnWebsocketMessageReceived(IEventParser eventParser,
    IEventHandler eventHandler,
    IApiHandler apiHandler): PluginBase, IWebSocketReceivedPlugin
{

    public Task OnWebSocketReceived(IWebSocket webSocket, WSDataFrameEventArgs e)
    {
        var text = e.DataFrame.ToText();
        eventHandler.LogReceived(Core.Data.LogLevel.Debug, $"接收到消息: {text}");

        if (!string.IsNullOrWhiteSpace(text))
        {
            try
            {
                using var doc = JsonDocument.Parse(text);
                if (doc.RootElement.TryGetProperty("retcode", out _))
                {
                    var actionResponse = JsonSerializer.Deserialize<ActionResponse>(text);
                    if (actionResponse != null)
                    {
                        if (actionResponse.RetCode != 0)
                        {
                            eventHandler.LogReceived(Core.Data.LogLevel.Error,
                                $"动作失败: {actionResponse.RetCode} - {actionResponse.Message}");
                        }
                        apiHandler.Dispatch(actionResponse);
                        return EasyTask.CompletedTask;
                    }
                }
            }
            catch (Exception ex)
            {
                eventHandler.LogReceived(Core.Data.LogLevel.Debug,
                    $"解析动作响应时出现异常: {ex.Message}");
            }
        }

        eventParser.ParseEvent(text ?? "");
        return EasyTask.CompletedTask;
    }
}