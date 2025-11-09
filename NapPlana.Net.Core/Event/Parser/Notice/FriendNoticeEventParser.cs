using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

public class FriendNoticeEventParser: NoticeEventParser
{
    public override void ParseEvent(string botEvent)
    {
        var baseEvent = JsonSerializer.Deserialize<NoticeEventBase>(botEvent);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot好友通知事件格式");
        }

        switch (baseEvent.NoticeType)
        {
            case NoticeType.FriendAdd:
            {
                var addEvent = JsonSerializer.Deserialize<FriendAddNoticeEvent>(botEvent);
                if (addEvent == null)
                    throw new UnSupportFeatureException("好友添加事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"收到好友添加通知: 用户 {addEvent.UserId}");
                break;
            }
            case NoticeType.FriendRecall:
            {
                var recallEvent = JsonSerializer.Deserialize<FriendRecallNoticeEvent>(botEvent);
                if (recallEvent == null)
                    throw new UnSupportFeatureException("好友消息撤回事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"收到好友消息撤回通知: 用户 {recallEvent.UserId} 消息ID {recallEvent.MessageId}");
                break;
            }
            default:
                // 不是好友相关，忽略
                break;
        }
    }
}

