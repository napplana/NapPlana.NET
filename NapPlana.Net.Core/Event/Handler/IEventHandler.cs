using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Data.Event.Meta;
using NapPlana.Core.Data.Event.Notice;

namespace NapPlana.Core.Event.Handler;

/// <summary>
/// 机器人事件处理器接口
/// </summary>
public interface IEventHandler
{
    #region 日志事件
    
    /// <summary>
    /// 日志通知事件
    /// </summary>
    event Action<LogLevel, string>? OnLogReceived;
    
    /// <summary>
    /// 触发日志接收事件
    /// </summary>
    void LogReceived(LogLevel logLevel, string message);
    
    #endregion
    
    #region 元事件
    
    /// <summary>
    /// 机器人 - 上线
    /// </summary>
    event Action? OnBotConnected;
    
    /// <summary>
    /// 触发机器人连接事件
    /// </summary>
    void BotConnected();
    
    /// <summary>
    /// 机器人 - 心跳
    /// </summary>
    event Action<HeartBeatEvent>? OnBotHeartbeat;
    
    /// <summary>
    /// 触发心跳事件
    /// </summary>
    void BotHeartbeat(HeartBeatEvent ev);
    
    /// <summary>
    /// 机器人 - 生命周期事件
    /// </summary>
    event Action<LifeCycleEvent>? OnBotLifeCycle;
    
    /// <summary>
    /// 触发生命周期事件
    /// </summary>
    void BotLifeCycle(LifeCycleEvent ev);
    
    #endregion
    
    #region 群聊消息
    
    /// <summary>
    /// 信息 - 群组
    /// </summary>
    event Action<GroupMessageEvent>? OnGroupMessageReceived;
    
    /// <summary>
    /// 触发群消息接收事件
    /// </summary>
    void GroupMessageReceived(GroupMessageEvent ev);
    
    #endregion
    
    #region 私信消息
    
    /// <summary>
    /// 消息 - 私信
    /// </summary>
    event Action<PrivateMessageEvent>? OnPrivateMessageReceived;
    
    /// <summary>
    /// 触发私信接收事件
    /// </summary>
    void PrivateMessageReceived(PrivateMessageEvent ev);
    
    /// <summary>
    /// 消息 - 私信 - 临时会话
    /// </summary>
    event Action<PrivateMessageEvent>? OnPrivateMessageReceivedTemporary;
    
    /// <summary>
    /// 触发临时会话私信接收事件
    /// </summary>
    void PrivateMessageReceivedTemporary(PrivateMessageEvent ev);
    
    /// <summary>
    /// 消息 - 私信 - 好友
    /// </summary>
    event Action<PrivateMessageEvent>? OnPrivateMessageReceivedFriend;
    
    /// <summary>
    /// 触发好友私信接收事件
    /// </summary>
    void PrivateMessageReceivedFriend(PrivateMessageEvent ev);
    
    #endregion
    
    #region 自身发送
    
    /// <summary>
    /// 消息发送 - 群聊
    /// </summary>
    event Action<MessageSentEvent>? OnMessageSentGroup;
    
    /// <summary>
    /// 触发群消息发送事件
    /// </summary>
    void MessageSentGroup(MessageSentEvent ev);
    
    /// <summary>
    /// 消息发送 - 私聊
    /// </summary>
    event Action<PrivateMessageSentEvent>? OnMessageSentPrivate;
    
    /// <summary>
    /// 触发私聊消息发送事件
    /// </summary>
    void MessageSentPrivate(PrivateMessageSentEvent ev);
    
    /// <summary>
    /// 消息发送 - 私聊 - 临时会话
    /// </summary>
    event Action<PrivateMessageSentEvent>? OnMessageSentPrivateTemporary;
    
    /// <summary>
    /// 触发临时会话消息发送事件
    /// </summary>
    void MessageSentTemporary(PrivateMessageSentEvent ev);
    
    /// <summary>
    /// 消息发送 - 私聊 - 好友
    /// </summary>
    event Action<PrivateMessageSentEvent>? OnMessageSentPrivateFriend;
    
    /// <summary>
    /// 触发好友消息发送事件
    /// </summary>
    void MessageSentPrivateFriend(PrivateMessageSentEvent ev);
    
    #endregion
    
    #region 通知事件 - 好友相关
    
    /// <summary>
    /// 通知 - 好友添加
    /// </summary>
    event Action<FriendAddNoticeEvent>? OnFriendAddNoticeReceived;
    
    /// <summary>
    /// 触发好友添加通知事件
    /// </summary>
    void FriendAddNoticeReceived(FriendAddNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 好友消息撤回
    /// </summary>
    event Action<FriendRecallNoticeEvent>? OnFriendRecallNoticeReceived;
    
    /// <summary>
    /// 触发好友消息撤回通知事件
    /// </summary>
    void FriendRecallNoticeReceived(FriendRecallNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群管理员
    
    /// <summary>
    /// 通知 - 群管理员变动 (总)
    /// </summary>
    event Action<GroupAdminNoticeEvent>? OnGroupAdminNoticeReceived;
    
    /// <summary>
    /// 触发群管理员变动通知事件
    /// </summary>
    void GroupAdminNoticeReceived(GroupAdminNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群管理员设置
    /// </summary>
    event Action<GroupAdminNoticeEvent>? OnGroupAdminSetNoticeReceived;
    
    /// <summary>
    /// 触发群管理员设置通知事件
    /// </summary>
    void GroupAdminSetNoticeReceived(GroupAdminNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群管理员取消
    /// </summary>
    event Action<GroupAdminNoticeEvent>? OnGroupAdminUnsetNoticeReceived;
    
    /// <summary>
    /// 触发群管理员取消通知事件
    /// </summary>
    void GroupAdminUnsetNoticeReceived(GroupAdminNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群禁言
    
    /// <summary>
    /// 通知 - 群禁言 (总)
    /// </summary>
    event Action<GroupBanNoticeEvent>? OnGroupBanNoticeReceived;
    
    /// <summary>
    /// 触发群禁言通知事件
    /// </summary>
    void GroupBanNoticeReceived(GroupBanNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群禁言 - 禁言
    /// </summary>
    event Action<GroupBanNoticeEvent>? OnGroupBanSetNoticeReceived;
    
    /// <summary>
    /// 触发群禁言设置通知事件
    /// </summary>
    void GroupBanSetNoticeReceived(GroupBanNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群禁言 - 解除
    /// </summary>
    event Action<GroupBanNoticeEvent>? OnGroupBanLiftNoticeReceived;
    
    /// <summary>
    /// 触发群禁言解除通知事件
    /// </summary>
    void GroupBanLiftNoticeReceived(GroupBanNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群成员名片
    
    /// <summary>
    /// 通知 - 群成员名片更新
    /// </summary>
    event Action<GroupCardEvent>? OnGroupCardNoticeReceived;
    
    /// <summary>
    /// 触发群成员名片更新通知事件
    /// </summary>
    void GroupCardNoticeReceived(GroupCardEvent ev);
    
    #endregion
    
    #region 通知事件 - 群成员减少
    
    /// <summary>
    /// 通知 - 群成员减少 (总)
    /// </summary>
    event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseNoticeReceived;
    
    /// <summary>
    /// 触发群成员减少通知事件
    /// </summary>
    void GroupDecreaseNoticeReceived(GroupDecreaseNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群成员减少 - 主动退群
    /// </summary>
    event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseLeaveNoticeReceived;
    
    /// <summary>
    /// 触发群成员主动退群通知事件
    /// </summary>
    void GroupDecreaseLeaveNoticeReceived(GroupDecreaseNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群成员减少 - 成员被踢
    /// </summary>
    event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickNoticeReceived;
    
    /// <summary>
    /// 触发群成员被踢通知事件
    /// </summary>
    void GroupDecreaseKickNoticeReceived(GroupDecreaseNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群成员减少 - 登录号被踢
    /// </summary>
    event Action<GroupDecreaseNoticeEvent>? OnGroupDecreaseKickMeNoticeReceived;
    
    /// <summary>
    /// 触发登录号被踢通知事件
    /// </summary>
    void GroupDecreaseKickMeNoticeReceived(GroupDecreaseNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群成员增加
    
    /// <summary>
    /// 通知 - 群成员增加 (总)
    /// </summary>
    event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseNoticeReceived;
    
    /// <summary>
    /// 触发群成员增加通知事件
    /// </summary>
    void GroupIncreaseNoticeReceived(GroupIncreaseNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群成员增加 - 管理员同意
    /// </summary>
    event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseApproveNoticeReceived;
    
    /// <summary>
    /// 触发群成员管理员同意通知事件
    /// </summary>
    void GroupIncreaseApproveNoticeReceived(GroupIncreaseNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群成员增加 - 管理员邀请
    /// </summary>
    event Action<GroupIncreaseNoticeEvent>? OnGroupIncreaseInviteNoticeReceived;
    
    /// <summary>
    /// 触发群成员管理员邀请通知事件
    /// </summary>
    void GroupIncreaseInviteNoticeReceived(GroupIncreaseNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群消息撤回
    
    /// <summary>
    /// 通知 - 群消息撤回
    /// </summary>
    event Action<GroupRecallNoticeEvent>? OnGroupRecallNoticeReceived;
    
    /// <summary>
    /// 触发群消息撤回通知事件
    /// </summary>
    void GroupRecallNoticeReceived(GroupRecallNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群文件上传
    
    /// <summary>
    /// 通知 - 群文件上传
    /// </summary>
    event Action<GroupUploadNoticeEvent>? OnGroupUploadNoticeReceived;
    
    /// <summary>
    /// 触发群文件上传通知事件
    /// </summary>
    void GroupUploadNoticeReceived(GroupUploadNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群精华消息
    
    /// <summary>
    /// 通知 - 群精华消息 (总)
    /// </summary>
    event Action<GroupEssenceNoticeEvent>? OnGroupEssenceNoticeReceived;
    
    /// <summary>
    /// 触发群精华消息通知事件
    /// </summary>
    void GroupEssenceNoticeReceived(GroupEssenceNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群精华消息增加
    /// </summary>
    event Action<GroupEssenceNoticeEvent>? OnGroupEssenceAddNoticeReceived;
    
    /// <summary>
    /// 触发群精华消息增加通知事件
    /// </summary>
    void GroupEssenceAddNoticeReceived(GroupEssenceNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群精华消息移除
    /// </summary>
    event Action<GroupEssenceNoticeEvent>? OnGroupEssenceDeleteNoticeReceived;
    
    /// <summary>
    /// 触发群精华消息移除通知事件
    /// </summary>
    void GroupEssenceDeleteNoticeReceived(GroupEssenceNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - 群消息表情点赞
    
    /// <summary>
    /// 通知 - 群消息表情点赞
    /// </summary>
    event Action<GroupMsgEmojiLikeNoticeEvent>? OnGroupMsgEmojiLikeNoticeReceived;
    
    /// <summary>
    /// 触发群消息表情点赞通知事件
    /// </summary>
    void GroupMsgEmojiLikeNoticeReceived(GroupMsgEmojiLikeNoticeEvent ev);
    
    #endregion
    
    #region 通知事件 - Notify子类型
    
    /// <summary>
    /// 通知 - 戳一戳 - 好友
    /// </summary>
    event Action<FriendPokeNoticeEvent>? OnFriendPokeNoticeReceived;
    
    /// <summary>
    /// 触发好友戳一戳通知事件
    /// </summary>
    void FriendPokeNoticeReceived(FriendPokeNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 戳一戳 - 群聊
    /// </summary>
    event Action<GroupPokeNoticeEvent>? OnGroupPokeNoticeReceived;
    
    /// <summary>
    /// 触发群聊戳一戳通知事件
    /// </summary>
    void GroupPokeNoticeReceived(GroupPokeNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 输入状态更新
    /// </summary>
    event Action<InputStatusNoticeEvent>? OnInputStatusNoticeReceived;
    
    /// <summary>
    /// 触发输入状态更新通知事件
    /// </summary>
    void InputStatusNoticeReceived(InputStatusNoticeEvent ev);
    
    /// <summary>
    /// 通知 - 群成员头衔变更
    /// </summary>
    event Action<GroupTitleEvent>? OnGroupTitleNoticeReceived;
    
    /// <summary>
    /// 触发群成员头衔变更通知事件
    /// </summary>
    void GroupTitleNoticeReceived(GroupTitleEvent ev);
    
    /// <summary>
    /// 通知 - 点赞
    /// </summary>
    event Action<ProfileLikeNoticeEvent>? OnProfileLikeNoticeReceived;
    
    /// <summary>
    /// 触发点赞通知事件
    /// </summary>
    void ProfileLikeNoticeReceived(ProfileLikeNoticeEvent ev);
    
    #endregion
}