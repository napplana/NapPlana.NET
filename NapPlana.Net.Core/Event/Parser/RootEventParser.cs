using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event;
using NapPlana.Core.Event.Parser.Meta;
using NapPlana.Core.Event.Parser.Notice;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser;

/// <summary>
/// 检查数据是否为OneBotEvent
/// </summary>
public class RootEventParser: IEventParser
{
    public virtual void ParseEvent(string jsonEventData)
    {
        var oneBotEvent = JsonSerializer.Deserialize<OneBotEvent>(jsonEventData);
        if (oneBotEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot事件格式");
        }

        switch (oneBotEvent.PostType)
        {
            case EventType.None:
                return;
            case EventType.Meta:
                // 处理Meta事件
                var metaEventParser = new MetaEventParser();
                metaEventParser.ParseEvent(jsonEventData);
                break;
            case EventType.Message:
                // 处理Message事件
                break;
            case EventType.MessageSent:
                // 处理MessageSent事件
                break;
            case EventType.Notice:
                // 处理Notice事件
                var noticeEventParser = new NoticeEventParser();
                noticeEventParser.ParseEvent(jsonEventData);
                break;
            case EventType.Request:
                // 处理Request事件
                break;
        }
    }
}