using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Data.Event.Notice;

namespace NapPlana.Core.Event.Handler;

/// <summary>
/// 机器人事件处理器 - 静态门面
/// </summary>
/// <remarks>
/// 此类提供静态API以保持向后兼容性。
/// 对于新项目，建议使用依赖注入方式注入IEventHandler。
/// </remarks>
public static class BotEventHandler
{
    private static IEventHandler _instance = new EventHandler();
    
    /// <summary>
    /// 设置事件处理器实例 (供DI使用)
    /// </summary>
    /// <param name="instance">事件处理器实例</param>
    public static void SetInstance(IEventHandler instance)
    {
        _instance = instance ?? throw new ArgumentNullException(nameof(instance));
    }
    
    /// <summary>
    /// 获取当前事件处理器实例
    /// </summary>
    public static IEventHandler GetInstance() => _instance;
    
    #region 日志事件
    
    /// <summary>
    /// 日志通知事件
    /// </summary>
    public static event Action<LogLevel, string>? OnLogReceived
    {
        add => _instance.OnLogReceived += value;
        remove => _instance.OnLogReceived -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="logLevel">日志等级</param>
    /// <param name="message">消息</param>
    public static void LogReceived(LogLevel logLevel, string message)
    {
        _instance.LogReceived(logLevel, message);
    }
    
    #endregion
    
    #region 元事件
    
    /// <summary>
    /// 机器人 - 上线
    /// </summary>
    public static event Action? OnBotConnected
    {
        add => _instance.OnBotConnected += value;
        remove => _instance.OnBotConnected -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    public static void BotConnected()
    {
        _instance.BotConnected();
    }
    
    /// <summary>
    /// 机器人 - 心跳
    /// </summary>
    public static event Action<HeartBeatEvent>? OnBotHeartbeat
    {
        add => _instance.OnBotHeartbeat += value;
        remove => _instance.OnBotHeartbeat -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void BotHeartbeat(HeartBeatEvent ev)
    {
        _instance.BotHeartbeat(ev);
    }
    
    /// <summary>
    /// 机器人 - 生命周期事件
    /// </summary>
    public static event Action<LifeCycleEvent>? OnBotLifeCycle
    {
        add => _instance.OnBotLifeCycle += value;
        remove => _instance.OnBotLifeCycle -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void BotLifeCycle(LifeCycleEvent ev)
    {
        _instance.BotLifeCycle(ev);
    }

    #endregion
    

    #region 群聊消息

    /// <summary>
    /// 信息 - 群组
    /// </summary>
    public static event Action<GroupMessageEvent>? OnGroupMessageReceived
    {
        add => _instance.OnGroupMessageReceived += value;
        remove => _instance.OnGroupMessageReceived -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupMessageReceived(GroupMessageEvent ev)
    {
        _instance.GroupMessageReceived(ev);
    }

    #endregion

    #region 私信消息

    /// <summary>
    /// 消息 - 私信
    /// </summary>
    public static event Action<PrivateMessageEvent>? OnPrivateMessageReceived
    {
        add => _instance.OnPrivateMessageReceived += value;
        remove => _instance.OnPrivateMessageReceived -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void PrivateMessageReceived(PrivateMessageEvent ev)
    {
        _instance.PrivateMessageReceived(ev);
    }
    
    /// <summary>
    /// 消息 - 私信 - 临时会话
    /// </summary>
    public static event Action<PrivateMessageEvent>? OnPrivateMessageReceivedTemporary
    {
        add => _instance.OnPrivateMessageReceivedTemporary += value;
        remove => _instance.OnPrivateMessageReceivedTemporary -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void PrivateMessageReceivedTemporary(PrivateMessageEvent ev)
    {
        _instance.PrivateMessageReceivedTemporary(ev);
    }
    
    /// <summary>
    /// 消息 - 私信 - 好友
    /// </summary>
    public static event Action<PrivateMessageEvent>? OnPrivateMessageReceivedFriend
    {
        add => _instance.OnPrivateMessageReceivedFriend += value;
        remove => _instance.OnPrivateMessageReceivedFriend -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void PrivateMessageReceivedFriend(PrivateMessageEvent ev)
    {
        _instance.PrivateMessageReceivedFriend(ev);
    }

    #endregion

    #region 自身发送

    /// <summary>
    /// 消息发送 - 群聊
    /// </summary>
    public static event Action<MessageSentEvent>? OnMessageSentGroup
    {
        add => _instance.OnMessageSentGroup += value;
        remove => _instance.OnMessageSentGroup -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void MessageSentGroup(MessageSentEvent ev)
    {
        _instance.MessageSentGroup(ev);
    }
    
    /// <summary>
    /// 消息发送 - 私聊
    /// </summary>
    public static event Action<PrivateMessageSentEvent>? OnMessageSentPrivate
    {
        add => _instance.OnMessageSentPrivate += value;
        remove => _instance.OnMessageSentPrivate -= value;
    }
    
    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void MessageSentPrivate(PrivateMessageSentEvent ev)
    {
        _instance.MessageSentPrivate(ev);
    }
    
    /// <summary>
    /// 消息发送 - 私聊 - 临时会话
    /// </summary>
    public static event Action<PrivateMessageSentEvent>? OnMessageSentPrivateTemporary
    {
        add => _instance.OnMessageSentPrivateTemporary += value;
        remove => _instance.OnMessageSentPrivateTemporary -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void MessageSentTemporary(PrivateMessageSentEvent ev)
    {
        _instance.MessageSentTemporary(ev);
    }
    
    /// <summary>
    /// 消息发送 - 私聊 - 好友
    /// </summary>
    public static event Action<PrivateMessageSentEvent>? OnMessageSentPrivateFriend
    {
        add => _instance.OnMessageSentPrivateFriend += value;
        remove => _instance.OnMessageSentPrivateFriend -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void MessageSentPrivateFriend(PrivateMessageSentEvent ev)
    {
        _instance.MessageSentPrivateFriend(ev);
    }

    #endregion

    #region 通知事件
    // ================== 好友相关 ==================
    /// <summary>
    /// 通知 - 好友添加 (notice.friend_add)
    /// </summary>
    public static event Action<FriendAddNoticeEvent>? OnFriendAddNoticeReceived
    {
        add => _instance.OnFriendAddNoticeReceived += value;
        remove => _instance.OnFriendAddNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void FriendAddNoticeReceived(FriendAddNoticeEvent ev)
    {
        _instance.FriendAddNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 好友消息撤回 (notice.friend_recall)
    /// </summary>
    public static event Action<FriendRecallNoticeEvent>? OnFriendRecallNoticeReceived
    {
        add => _instance.OnFriendRecallNoticeReceived += value;
        remove => _instance.OnFriendRecallNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void FriendRecallNoticeReceived(FriendRecallNoticeEvent ev)
    {
        _instance.FriendRecallNoticeReceived(ev);
    }

    // ================== 群管理员 ==================
    /// <summary>
    /// 通知 - 群管理员变动 (总) (notice.group_admin)
    /// </summary>
    public static event Action<GroupAdminNoticeEvent>? OnGroupAdminNoticeReceived
    {
        add => _instance.OnGroupAdminNoticeReceived += value;
        remove => _instance.OnGroupAdminNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupAdminNoticeReceived(GroupAdminNoticeEvent ev)
    {
        _instance.GroupAdminNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群管理员设置 (notice.group_admin.set)
    /// </summary>
    public static event Action<GroupAdminNoticeEvent>? OnGroupAdminSetNoticeReceived
    {
        add => _instance.OnGroupAdminSetNoticeReceived += value;
        remove => _instance.OnGroupAdminSetNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupAdminSetNoticeReceived(GroupAdminNoticeEvent ev)
    {
        _instance.GroupAdminSetNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群管理员取消 (notice.group_admin.unset)
    /// </summary>
    public static event Action<GroupAdminNoticeEvent>? OnGroupAdminUnsetNoticeReceived
    {
        add => _instance.OnGroupAdminUnsetNoticeReceived += value;
        remove => _instance.OnGroupAdminUnsetNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupAdminUnsetNoticeReceived(GroupAdminNoticeEvent ev)
    {
        _instance.GroupAdminUnsetNoticeReceived(ev);
    }

    // ================== 群禁言 ==================
    /// <summary>
    /// 通知 - 群禁言 (总) (notice.group_ban)
    /// </summary>
    public static event Action<GroupBanNoticeEvent>? OnGroupBanNoticeReceived
    {
        add => _instance.OnGroupBanNoticeReceived += value;
        remove => _instance.OnGroupBanNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupBanNoticeReceived(GroupBanNoticeEvent ev)
    {
        _instance.GroupBanNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群禁言 - 禁言 (notice.group_ban.ban)
    /// </summary>
    public static event Action<GroupBanNoticeEvent>? OnGroupBanSetNoticeReceived
    {
        add => _instance.OnGroupBanSetNoticeReceived += value;
        remove => _instance.OnGroupBanSetNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupBanSetNoticeReceived(GroupBanNoticeEvent ev)
    {
        _instance.GroupBanSetNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群禁言 - 解除 (notice.group_ban.lift_ban)
    /// </summary>
    public static event Action<GroupBanNoticeEvent>? OnGroupBanLiftNoticeReceived
    {
        add => _instance.OnGroupBanLiftNoticeReceived += value;
        remove => _instance.OnGroupBanLiftNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupBanLiftNoticeReceived(GroupBanNoticeEvent ev)
    {
        _instance.GroupBanLiftNoticeReceived(ev);
    }

    // ================== 群成员名片 ==================
    /// <summary>
    /// 通知 - 群成员名片更新 (notice.group_card)
    /// </summary>
    public static event Action<GroupCardEvent>? OnGroupCardNoticeReceived
    {
        add => _instance.OnGroupCardNoticeReceived += value;
        remove => _instance.OnGroupCardNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupCardNoticeReceived(GroupCardEvent ev)
    {
        _instance.GroupCardNoticeReceived(ev);
    }

    // ================== 群成员减少 ==================
    /// <summary>
    /// 通知 - 群成员减少 (总) (notice.group_decrease)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseNoticeReceived
    {
        add => _instance.OnGroupDecreaseNoticeReceived += value;
        remove => _instance.OnGroupDecreaseNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupDecreaseNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        _instance.GroupDecreaseNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群成员减少 - 主动退群 (notice.group_decrease.leave)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseLeaveNoticeReceived
    {
        add => _instance.OnGroupDecreaseLeaveNoticeReceived += value;
        remove => _instance.OnGroupDecreaseLeaveNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupDecreaseLeaveNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        _instance.GroupDecreaseLeaveNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群成员减少 - 成员被踢 (notice.group_decrease.kick)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickNoticeReceived
    {
        add => _instance.OnGroupDecreaseKickNoticeReceived += value;
        remove => _instance.OnGroupDecreaseKickNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupDecreaseKickNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        _instance.GroupDecreaseKickNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群成员减少 - 登录号被踢 (notice.group_decrease.kick_me)
    /// </summary>
    public static event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickMeNoticeReceived
    {
        add => _instance.OnGroupDecreaseKickMeNoticeReceived += value;
        remove => _instance.OnGroupDecreaseKickMeNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupDecreaseKickMeNoticeReceived(GroupDecreaseNoticeEvent ev)
    {
        _instance.GroupDecreaseKickMeNoticeReceived(ev);
    }

    // ================== 群成员增加 ==================
    /// <summary>
    /// 通知 - 群成员增加 (总) (notice.group_increase)
    /// </summary>
    public static event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseNoticeReceived
    {
        add => _instance.OnGroupIncreaseNoticeReceived += value;
        remove => _instance.OnGroupIncreaseNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupIncreaseNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        _instance.GroupIncreaseNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群成员增加 - 管理员同意 (notice.group_increase.approve)
    /// </summary>
    public static event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseApproveNoticeReceived
    {
        add => _instance.OnGroupIncreaseApproveNoticeReceived += value;
        remove => _instance.OnGroupIncreaseApproveNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupIncreaseApproveNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        _instance.GroupIncreaseApproveNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群成员增加 - 管理员邀请 (notice.group_increase.invite)
    /// </summary>
    public static event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseInviteNoticeReceived
    {
        add => _instance.OnGroupIncreaseInviteNoticeReceived += value;
        remove => _instance.OnGroupIncreaseInviteNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupIncreaseInviteNoticeReceived(GroupIncreaseNoticeEvent ev)
    {
        _instance.GroupIncreaseInviteNoticeReceived(ev);
    }

    // ================== 群消息撤回 ==================
    /// <summary>
    /// 通知 - 群消息撤回 (notice.group_recall)
    /// </summary>
    public static event Action<GroupRecallNoticeEvent>? OnGroupRecallNoticeReceived
    {
        add => _instance.OnGroupRecallNoticeReceived += value;
        remove => _instance.OnGroupRecallNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupRecallNoticeReceived(GroupRecallNoticeEvent ev)
    {
        _instance.GroupRecallNoticeReceived(ev);
    }

    // ================== 群文件上传 ==================
    /// <summary>
    /// 通知 - 群文件上传 (notice.group_upload)
    /// </summary>
    public static event Action<GroupUploadNoticeEvent>? OnGroupUploadNoticeReceived
    {
        add => _instance.OnGroupUploadNoticeReceived += value;
        remove => _instance.OnGroupUploadNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupUploadNoticeReceived(GroupUploadNoticeEvent ev)
    {
        _instance.GroupUploadNoticeReceived(ev);
    }

    // ================== 群精华消息 ==================
    /// <summary>
    /// 通知 - 群精华消息 (总) (notice.essence)
    /// </summary>
    public static event Action<GroupEssenceNoticeEvent>? OnGroupEssenceNoticeReceived
    {
        add => _instance.OnGroupEssenceNoticeReceived += value;
        remove => _instance.OnGroupEssenceNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupEssenceNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        _instance.GroupEssenceNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群精华消息增加 (notice.essence.add)
    /// </summary>
    public static event Action<GroupEssenceNoticeEvent>? OnGroupEssenceAddNoticeReceived
    {
        add => _instance.OnGroupEssenceAddNoticeReceived += value;
        remove => _instance.OnGroupEssenceAddNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupEssenceAddNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        _instance.GroupEssenceAddNoticeReceived(ev);
    }
    
    /// <summary>
    /// 通知 - 群精华消息移除 (notice.essence.delete)
    /// </summary>
    public static event Action<GroupEssenceNoticeEvent>? OnGroupEssenceDeleteNoticeReceived
    {
        add => _instance.OnGroupEssenceDeleteNoticeReceived += value;
        remove => _instance.OnGroupEssenceDeleteNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupEssenceDeleteNoticeReceived(GroupEssenceNoticeEvent ev)
    {
        _instance.GroupEssenceDeleteNoticeReceived(ev);
    }

    // ================== 群消息表情点赞 ==================
    /// <summary>
    /// 通知 - 群消息表情点赞 (notice.group_msg_emoji_like)
    /// </summary>
    public static event Action<GroupMsgEmojiLikeNoticeEvent>? OnGroupMsgEmojiLikeNoticeReceived
    {
        add => _instance.OnGroupMsgEmojiLikeNoticeReceived += value;
        remove => _instance.OnGroupMsgEmojiLikeNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupMsgEmojiLikeNoticeReceived(GroupMsgEmojiLikeNoticeEvent ev)
    {
        _instance.GroupMsgEmojiLikeNoticeReceived(ev);
    }
    
    // ================== Notify子类型 ==================
    /// <summary>
    /// 通知 - 戳一戳 (notice.notify.poke) - 好友
    /// </summary>
    public static event Action<FriendPokeNoticeEvent>? OnFriendPokeNoticeReceived
    {
        add => _instance.OnFriendPokeNoticeReceived += value;
        remove => _instance.OnFriendPokeNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void FriendPokeNoticeReceived(FriendPokeNoticeEvent ev)
    {
        _instance.FriendPokeNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 戳一戳 (notice.notify.poke) - 群聊
    /// </summary>
    public static event Action<GroupPokeNoticeEvent>? OnGroupPokeNoticeReceived
    {
        add => _instance.OnGroupPokeNoticeReceived += value;
        remove => _instance.OnGroupPokeNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupPokeNoticeReceived(GroupPokeNoticeEvent ev)
    {
        _instance.GroupPokeNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 输入状态更新 (notice.notify.input_status)
    /// </summary>
    public static event Action<InputStatusNoticeEvent>? OnInputStatusNoticeReceived
    {
        add => _instance.OnInputStatusNoticeReceived += value;
        remove => _instance.OnInputStatusNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void InputStatusNoticeReceived(InputStatusNoticeEvent ev)
    {
        _instance.InputStatusNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 群成员头衔变更 (notice.notify.title)
    /// </summary>
    public static event Action<GroupTitleEvent>? OnGroupTitleNoticeReceived
    {
        add => _instance.OnGroupTitleNoticeReceived += value;
        remove => _instance.OnGroupTitleNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void GroupTitleNoticeReceived(GroupTitleEvent ev)
    {
        _instance.GroupTitleNoticeReceived(ev);
    }

    /// <summary>
    /// 通知 - 点赞 (notice.notify.profile_like)
    /// </summary>
    public static event Action<ProfileLikeNoticeEvent>? OnProfileLikeNoticeReceived
    {
        add => _instance.OnProfileLikeNoticeReceived += value;
        remove => _instance.OnProfileLikeNoticeReceived -= value;
    }

    /// <summary>
    /// 系统内部调用，程序集外请勿使用
    /// </summary>
    /// <param name="ev">事件参数</param>
    public static void ProfileLikeNoticeReceived(ProfileLikeNoticeEvent ev)
    {
        _instance.ProfileLikeNoticeReceived(ev);
    }

    #endregion
    
}