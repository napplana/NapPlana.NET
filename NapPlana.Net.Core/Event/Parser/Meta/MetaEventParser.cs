using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Meta;

namespace NapPlana.Core.Event.Parser.Meta;

public class MetaEventParser: RootEventParser
{
    public override void ParseEvent(string botEvent)
    {

        var metaEvent = JsonSerializer.Deserialize<MetaEventBase>(botEvent);
        if (metaEvent == null)
        {
            throw new Exception("无法解析该事件数据，可能不是OneBot元事件格式");
        }
        switch (metaEvent.MetaEventType)
        {
            case MetaEventType.Lifecycle:
                var lifeCycleParser = new LifeCycleEventParser();
                lifeCycleParser.ParseEvent(botEvent);
                break;
            case MetaEventType.Heartbeat:
                var heartbeatParser = new HeartBeatEventParser();
                heartbeatParser.ParseEvent(botEvent);
                break;
        }
    }
}