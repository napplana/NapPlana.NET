using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

public class GroupNoticeEventParser: NoticeEventParser
{
    public override void ParseEvent(string botEvent)
    {
        var baseEvent = JsonSerializer.Deserialize<NoticeEventBase>(botEvent);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot群通知事件格式");
        }
        //密集堆积一下吧，写文件多了改命名空间会炸的
        switch (baseEvent.NoticeType)
        {
            case NoticeType.GroupRecall:
            {
                var recallEvent = JsonSerializer.Deserialize<GroupRecallNoticeEvent>(botEvent);
                if (recallEvent == null) throw new UnSupportFeatureException("群消息撤回事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{recallEvent.GroupId} 消息撤回: 操作者 {recallEvent.OperatorId} 消息ID {recallEvent.MessageId}");
                break;
            }
            case NoticeType.GroupIncrease:
            {
                var increaseEvent = JsonSerializer.Deserialize<GroupIncreaseNoticeEvent>(botEvent);
                if (increaseEvent == null) throw new UnSupportFeatureException("群成员增加事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{increaseEvent.GroupId} 成员增加: 用户 {increaseEvent.UserId} 操作类型 {increaseEvent.IncreaseType}");
                break;
            }
            case NoticeType.GroupDecrease:
            {
                var decreaseEvent = JsonSerializer.Deserialize<GroupDecreaseNoticeEvent>(botEvent);
                if (decreaseEvent == null) throw new UnSupportFeatureException("群成员减少事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{decreaseEvent.GroupId} 成员减少: 用户 {decreaseEvent.UserId} 操作类型 {decreaseEvent.DecreaseType}");
                break;
            }
            case NoticeType.GroupAdmin:
            {
                var adminEvent = JsonSerializer.Deserialize<GroupAdminNoticeEvent>(botEvent);
                if (adminEvent == null) throw new UnSupportFeatureException("群管理员变更事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{adminEvent.GroupId} 管理员变更: 用户 {adminEvent.UserId} 操作类型 {adminEvent.AdminType}");
                break;
            }
            case NoticeType.GroupBan:
            {
                var banEvent = JsonSerializer.Deserialize<GroupBanNoticeEvent>(botEvent);
                if (banEvent == null) throw new UnSupportFeatureException("群禁言事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{banEvent.GroupId} 禁言: 用户 {banEvent.UserId} 操作者 {banEvent.OperatorId} 禁言时长 {banEvent.Duration}s 类型 {banEvent.BanType}");
                break;
            }
            case NoticeType.GroupUpload:
            {
                var uploadEvent = JsonSerializer.Deserialize<GroupUploadNoticeEvent>(botEvent);
                if (uploadEvent == null) throw new UnSupportFeatureException("群文件上传事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{uploadEvent.GroupId} 文件上传: {uploadEvent.File.FileName} 大小 {uploadEvent.File.Size} 字节");
                break;
            }
            case NoticeType.GroupCard:
            {
                var cardEvent = JsonSerializer.Deserialize<GroupCardEvent>(botEvent);
                if (cardEvent == null) throw new UnSupportFeatureException("群名片变更事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{cardEvent.GroupId} 名片变更: 用户 {cardEvent.UserId} {cardEvent.OldCard} -> {cardEvent.NewCard}");
                break;
            }
            case NoticeType.Essence:
            {
                var essenceEvent = JsonSerializer.Deserialize<GroupEssenceNoticeEvent>(botEvent);
                if (essenceEvent == null) throw new UnSupportFeatureException("群精华消息事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{essenceEvent.GroupId} 精华消息: 消息ID {essenceEvent.MessageId} 操作类型 {essenceEvent.EssenceType} 操作者 {essenceEvent.OperatorId}");
                break;
            }
            case NoticeType.GroupMsgEmojiLike:
            {
                var likeEvent = JsonSerializer.Deserialize<GroupMsgEmojiLikeNoticeEvent>(botEvent);
                if (likeEvent == null) throw new UnSupportFeatureException("群消息表情回应事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{likeEvent.GroupId} 消息{likeEvent.MessageId} 表情点赞: 共 {likeEvent.Likes.Count} 种表情");
                break;
            }
            default:
                break; // 非群类型忽略
        }
    }
}

