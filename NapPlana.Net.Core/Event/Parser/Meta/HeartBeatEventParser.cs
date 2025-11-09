using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Core.Event.Parser.Meta;

public class HeartBeatEventParser:MetaEventParser
{
    public override void ParseEvent(string botEvent)
    {
        var heartBeatEvent = JsonSerializer.Deserialize<HeartBeatEvent>(botEvent);
        if (heartBeatEvent == null)
        {
            throw new Exception("无法解析该事件数据，可能不是OneBot心跳包事件格式");
        }
        
        BotEventHandler.LogReceived(LogLevel.Debug,$"收到心跳包，时间戳{heartBeatEvent.TimeStamp}");
    }
}