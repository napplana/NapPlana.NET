using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Core.Event.Parser.Meta;

public class LifeCycleEventParser: MetaEventParser
{
    public override void ParseEvent(string botEvent)
    {
        var lifeCycleEvent = JsonSerializer.Deserialize<LifeCycleEvent>(botEvent);
        if (lifeCycleEvent == null)
        {
            throw new Exception("无法解析该事件数据，可能不是OneBot生命周期事件格式");
        }

        switch (lifeCycleEvent.SubType)
        {
            case LifeCycleSubType.Connect:
                BotEventHandler.LogReceived(LogLevel.Info,$"机器人已连接到NapCat服务器");
                BotEventHandler.BotConnected();
                break;
            case LifeCycleSubType.Disable:
                BotEventHandler.LogReceived(LogLevel.Warning,$"机器人已与NapCat服务器断开连接");
                break;
            case LifeCycleSubType.Enable:
                BotEventHandler.LogReceived(LogLevel.Debug,$"机器人已启用");
                break;
        }
    }
}