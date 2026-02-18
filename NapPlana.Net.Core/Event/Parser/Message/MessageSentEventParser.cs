using System.Text.Json;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Event.Message;
using NapPlana.Core.Data.Message;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Exceptions;

namespace NapPlana.Core.Event.Parser.Message;

/// <summary>
/// 自身消息发送事件解析器
/// </summary>
public class MessageSentEventParser(IEventHandler handler) : MessageEventParser(handler)
{
    /// <summary>
    /// 解析事件是否为自身发送事件
    /// </summary>
    /// <param name="jsonEventData">数据</param>
    /// <exception cref="UnSupportFeatureException">不是自身发送事件</exception>
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<MessageSentEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot消息发送事件格式");
        }
        //不打日志了，从事件接收吧
        switch (ev.MessageType)
        {
            case MessageType.Private:
                var privateMsg = new PrivateMessageSentEventParser(handler);
                privateMsg.ParseEvent(jsonEventData);
                return;
            case MessageType.Group:
                handler.MessageSentGroup(ev);
                break;
        }
        
    }
}

/// <summary>
/// 私信消息自身发送事件解析器
/// </summary>
public class PrivateMessageSentEventParser(IEventHandler handler) : MessageSentEventParser(handler)
{
    /// <summary>
    /// 解析事件是否为私聊消息发送事件
    /// </summary>
    /// <param name="jsonEventData">数据</param>
    /// <exception cref="UnSupportFeatureException">不是对应事件</exception>
    public override void ParseEvent(string jsonEventData)
    {
        var ev = JsonSerializer.Deserialize<PrivateMessageSentEvent>(jsonEventData);
        if (ev == null)
        {
            throw new UnSupportFeatureException("无法解析该事件数据，可能不是OneBot私聊消息发送事件格式");
        }
        handler.MessageSentPrivate(ev);
        
        switch (ev.SubType)
        {
            //群临时会话，似乎是这样
            case PrivateMessageSubType.Group:
                if (ev.TempFlag.HasValue)
                {
                    handler.MessageSentTemporary(ev);
                }
                break;
            case PrivateMessageSubType.Friend:
                handler.MessageSentPrivateFriend(ev);
                break;
        }
    }
}
