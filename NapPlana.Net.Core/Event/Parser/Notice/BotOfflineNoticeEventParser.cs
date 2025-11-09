using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

public class BotOfflineNoticeEventParser: NoticeEventParser
{
    public override void ParseEvent(string botEvent)
    {
        var offlineEvent = JsonSerializer.Deserialize<BotOfflineEvent>(botEvent);
        if (offlineEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是机器人离线通知事件格式");
        }
        BotEventHandler.LogReceived(LogLevel.Warning, $"机器人离线: 用户 {offlineEvent.UserId} Tag {offlineEvent.Tag} 信息 {offlineEvent.Message}");
    }
}
