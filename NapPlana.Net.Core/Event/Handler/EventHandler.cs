using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Data.Event.Notice;

namespace NapPlana.Core.Event.Handler;

/// <summary>
/// 机器人事件处理器实现
/// </summary>
public class EventHandler : IEventHandler
{
    #region 日志事件
    
    /// <inheritdoc />
    public event Action<LogLevel, string>? OnLogReceived;
    
    /// <inheritdoc />
    public void LogReceived(LogLevel logLevel, string message)
    {
        OnLogReceived?.Invoke(logLevel, message);
    }
    
    #endregion
    
    #region 元事件
    
    /// <inheritdoc />
    public event Action? OnBotConnected;
    
    /// <inheritdoc />
    public void BotConnected()
    {
        OnBotConnected?.Invoke();
    }
    
    /// <inheritdoc />
    public event Action<HeartBeatEvent>? OnBotHeartbeat;
    
    /// <inheritdoc />
    public void BotHeartbeat(HeartBeatEvent ev)
    {
        OnBotHeartbeat?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<LifeCycleEvent>? OnBotLifeCycle;
    
    /// <inheritdoc />
    public void BotLifeCycle(LifeCycleEvent ev)
    {
        OnBotLifeCycle?.Invoke(ev);
    }
    
    #endregion
    
    #region 群聊消息
    
    /// <inheritdoc />
    public event Action<GroupMessageEvent>? OnGroupMessageReceived;
    
    /// <inheritdoc />
    public void GroupMessageReceived(GroupMessageEvent ev)
    {
        OnGroupMessageReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 私信消息
    
    /// <inheritdoc />
    public event Action<PrivateMessageEvent>? OnPrivateMessageReceived;
    
    /// <inheritdoc />
    public void PrivateMessageReceived(PrivateMessageEvent ev)
    {
        OnPrivateMessageReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<PrivateMessageEvent>? OnPrivateMessageReceivedTemporary;
    
    /// <inheritdoc />
    public void PrivateMessageReceivedTemporary(PrivateMessageEvent ev)
    {
        OnPrivateMessageReceivedTemporary?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<PrivateMessageEvent>? OnPrivateMessageReceivedFriend;
    
    /// <inheritdoc />
    public void PrivateMessageReceivedFriend(PrivateMessageEvent ev)
    {
        OnPrivateMessageReceivedFriend?.Invoke(ev);
    }
    
    #endregion
    
    #region 自身发送
    
    /// <inheritdoc />
    public event Action<MessageSentEvent>? OnMessageSentGroup;
    
    /// <inheritdoc />
    public void MessageSentGroup(MessageSentEvent ev)
    {
        OnMessageSentGroup?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<PrivateMessageSentEvent>? OnMessageSentPrivate;
    
    /// <inheritdoc />
    public void MessageSentPrivate(PrivateMessageSentEvent ev)
    {
        OnMessageSentPrivate?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<PrivateMessageSentEvent>? OnMessageSentPrivateTemporary;
    
    /// <inheritdoc />
    public void MessageSentTemporary(PrivateMessageSentEvent ev)
    {
        OnMessageSentPrivateTemporary?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<PrivateMessageSentEvent>? OnMessageSentPrivateFriend;
    
    /// <inheritdoc />
    public void MessageSentPrivateFriend(PrivateMessageSentEvent ev)
    {
        OnMessageSentPrivateFriend?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 好友相关
    
    /// <inheritdoc />
    public event Action<FriendAddNoticeEvent>? OnFriendAddNoticeReceived;
    
    /// <inheritdoc />
    public void FriendAddNoticeReceived(FriendAddNoticeEvent ev)
    {
        OnFriendAddNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<FriendRecallNoticeEvent>? OnFriendRecallNoticeReceived;
    
    /// <inheritdoc />
    public void FriendRecallNoticeReceived(FriendRecallNoticeEvent ev)
    {
        OnFriendRecallNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群管理员
    
    /// <inheritdoc />
    public event Action<GroupAdminNoticeEvent>? OnGroupAdminNoticeReceived;
    
    /// <inheritdoc />
    public void GroupAdminNoticeReceived(GroupAdminNoticeEvent ev)
    {
        OnGroupAdminNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupAdminNoticeEvent>? OnGroupAdminSetNoticeReceived;
    
    /// <inheritdoc />
    public void GroupAdminSetNoticeReceived(GroupAdminNoticeEvent ev)
    {
        OnGroupAdminSetNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupAdminNoticeEvent>? OnGroupAdminUnsetNoticeReceived;
    
    /// <inheritdoc />
    public void GroupAdminUnsetNoticeReceived(GroupAdminNoticeEvent ev)
    {
        OnGroupAdminUnsetNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群禁言
    
    /// <inheritdoc />
    public event Action<GroupBanNoticeEvent>? OnGroupBanNoticeReceived;
    
    /// <inheritdoc />
    public void GroupBanNoticeReceived(GroupBanNoticeEvent ev)
    {
        OnGroupBanNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupBanNoticeEvent>? OnGroupBanSetNoticeReceived;
    
    /// <inheritdoc />
    public void GroupBanSetNoticeReceived(GroupBanNoticeEvent ev)
    {
        OnGroupBanSetNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupBanNoticeEvent>? OnGroupBanLiftNoticeReceived;
    
    /// <inheritdoc />
    public void GroupBanLiftNoticeReceived(GroupBanNoticeEvent ev)
    {
        OnGroupBanLiftNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群成员名片
    
    /// <inheritdoc />
    public event Action<GroupCardEvent>? OnGroupCardNoticeReceived;
    
    /// <inheritdoc />
    public void GroupCardNoticeReceived(GroupCardEvent ev)
    {
        OnGroupCardNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群成员减少
    
    /// <inheritdoc />
    public event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseNoticeReceived;
    
    /// <inheritdoc />
    public void GroupDecreaseNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseLeaveNoticeReceived;
    
    /// <inheritdoc />
    public void GroupDecreaseLeaveNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseLeaveNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickNoticeReceived;
    
    /// <inheritdoc />
    public void GroupDecreaseKickNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseKickNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickMeNoticeReceived;
    
    /// <inheritdoc />
    public void GroupDecreaseKickMeNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        OnGroupDecreaseKickMeNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群成员增加
    
    /// <inheritdoc />
    public event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseNoticeReceived;
    
    /// <inheritdoc />
    public void GroupIncreaseNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        OnGroupIncreaseNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseApproveNoticeReceived;
    
    /// <inheritdoc />
    public void GroupIncreaseApproveNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        OnGroupIncreaseApproveNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseInviteNoticeReceived;
    
    /// <inheritdoc />
    public void GroupIncreaseInviteNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        OnGroupIncreaseInviteNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群消息撤回
    
    /// <inheritdoc />
    public event Action<GroupRecallNoticeEvent>? OnGroupRecallNoticeReceived;
    
    /// <inheritdoc />
    public void GroupRecallNoticeReceived(GroupRecallNoticeEvent ev)
    {
        OnGroupRecallNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群文件上传
    
    /// <inheritdoc />
    public event Action<GroupUploadNoticeEvent>? OnGroupUploadNoticeReceived;
    
    /// <inheritdoc />
    public void GroupUploadNoticeReceived(GroupUploadNoticeEvent ev)
    {
        OnGroupUploadNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群精华消息
    
    /// <inheritdoc />
    public event Action<GroupEssenceNoticeEvent>? OnGroupEssenceNoticeReceived;
    
    /// <inheritdoc />
    public void GroupEssenceNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        OnGroupEssenceNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupEssenceNoticeEvent>? OnGroupEssenceAddNoticeReceived;
    
    /// <inheritdoc />
    public void GroupEssenceAddNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        OnGroupEssenceAddNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupEssenceNoticeEvent>? OnGroupEssenceDeleteNoticeReceived;
    
    /// <inheritdoc />
    public void GroupEssenceDeleteNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        OnGroupEssenceDeleteNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - 群消息表情点赞
    
    /// <inheritdoc />
    public event Action<GroupMsgEmojiLikeNoticeEvent>? OnGroupMsgEmojiLikeNoticeReceived;
    
    /// <inheritdoc />
    public void GroupMsgEmojiLikeNoticeReceived(GroupMsgEmojiLikeNoticeEvent ev)
    {
        OnGroupMsgEmojiLikeNoticeReceived?.Invoke(ev);
    }
    
    #endregion
    
    #region 通知事件 - Notify子类型
    
    /// <inheritdoc />
    public event Action<FriendPokeNoticeEvent>? OnFriendPokeNoticeReceived;
    
    /// <inheritdoc />
    public void FriendPokeNoticeReceived(FriendPokeNoticeEvent ev)
    {
        OnFriendPokeNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupPokeNoticeEvent>? OnGroupPokeNoticeReceived;
    
    /// <inheritdoc />
    public void GroupPokeNoticeReceived(GroupPokeNoticeEvent ev)
    {
        OnGroupPokeNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<InputStatusNoticeEvent>? OnInputStatusNoticeReceived;
    
    /// <inheritdoc />
    public void InputStatusNoticeReceived(InputStatusNoticeEvent ev)
    {
        OnInputStatusNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<GroupTitleEvent>? OnGroupTitleNoticeReceived;
    
    /// <inheritdoc />
    public void GroupTitleNoticeReceived(GroupTitleEvent ev)
    {
        OnGroupTitleNoticeReceived?.Invoke(ev);
    }
    
    /// <inheritdoc />
    public event Action<ProfileLikeNoticeEvent>? OnProfileLikeNoticeReceived;
    
    /// <inheritdoc />
    public void ProfileLikeNoticeReceived(ProfileLikeNoticeEvent ev)
    {
        OnProfileLikeNoticeReceived?.Invoke(ev);
    }
    
    #endregion
}

