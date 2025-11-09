using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Notice;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Notice;

public class NotifyNoticeEventParser: NoticeEventParser
{
    public override void ParseEvent(string botEvent)
    {
        var baseEvent = JsonSerializer.Deserialize<NoticeEventBase>(botEvent);
        if (baseEvent == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot notify通知事件格式");
        }
        // notify包装的类型都应为 NoticeType.Notify
        if (baseEvent.NoticeType != NoticeType.Notify)
        {
            return;
        }

        //判断sub_type来进行具体反序列化
        //由于不同类中都存在SubType属性, 简单解析为 JsonDocument 再取 sub_type 字段
        using var doc = JsonDocument.Parse(botEvent);
        var root = doc.RootElement;
        var subTypeStr = root.TryGetProperty("sub_type", out var st) ? st.GetString() : string.Empty;
        switch (subTypeStr)
        {
            case "poke":
            {
                // 判断是否有 group_id 来区分好友还是群戳一戳
                if (root.TryGetProperty("group_id", out _))
                {
                    var groupPoke = JsonSerializer.Deserialize<GroupPokeNoticeEvent>(botEvent);
                    if (groupPoke == null) throw new UnSupportFeatureException("群戳一戳事件反序列化失败");
                    BotEventHandler.LogReceived(LogLevel.Info, $"群{groupPoke.GroupId} 戳一戳: 用户 {groupPoke.UserId} -> 目标 {groupPoke.TargetId}");
                }
                else
                {
                    var friendPoke = JsonSerializer.Deserialize<FriendPokeNoticeEvent>(botEvent);
                    if (friendPoke == null) throw new UnSupportFeatureException("好友戳一戳事件反序列化失败");
                    BotEventHandler.LogReceived(LogLevel.Info, $"好友戳一戳: 用户 {friendPoke.UserId} -> 目标 {friendPoke.TargetId}");
                }
                break;
            }
            case "profile_like":
            {
                var profileLike = JsonSerializer.Deserialize<ProfileLikeNoticeEvent>(botEvent);
                if (profileLike == null) throw new UnSupportFeatureException("资料点赞事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"资料点赞: 操作者 {profileLike.OperatorId} 次数 {profileLike.Times}");
                break;
            }
            case "input_status":
            {
                var inputStatus = JsonSerializer.Deserialize<InputStatusNoticeEvent>(botEvent);
                if (inputStatus == null) throw new UnSupportFeatureException("输入状态事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"输入状态: 用户 {inputStatus.UserId} 状态 {inputStatus.StatusText} 事件类型 {inputStatus.InputEventType}");
                break;
            }
            case "group_name":
            {
                var groupName = JsonSerializer.Deserialize<GroupNameEvent>(botEvent);
                if (groupName == null) throw new UnSupportFeatureException("群名变更事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{groupName.GroupId} 名称变更为 {groupName.NewName}");
                break;
            }
            case "title":
            {
                var groupTitle = JsonSerializer.Deserialize<GroupTitleEvent>(botEvent);
                if (groupTitle == null) throw new UnSupportFeatureException("群头衔变更事件反序列化失败");
                BotEventHandler.LogReceived(LogLevel.Info, $"群{groupTitle.GroupId} 新头衔 {groupTitle.Title}");
                break;
            }
            default:
                BotEventHandler.LogReceived(LogLevel.Debug, $"收到未识别的notify子类型: {subTypeStr}");
                break;
        }
    }
}

