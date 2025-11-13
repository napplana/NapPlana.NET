using NapPlana.Core.Data;
using NapPlana.Core.Data.Message;
using System; // added for Convert
using System.IO; // added for FileStream/MemoryStream

namespace NapPlana.Core.Bot;

/// <summary>
/// 消息链构筑器
/// </summary>
public class MessageChainBuilder
{
    /// <summary>
    /// 创建消息链构筑器
    /// </summary>
    /// <returns>构筑器实例</returns>
    public static MessageChainBuilder Create()  => new MessageChainBuilder();
    
    private List<MessageBase> _messages = new List<MessageBase>();
    
    public List<MessageBase> Build() => _messages;
    
    /// <summary>
    /// 添加文字消息
    /// </summary>
    /// <param name="text">文字</param>
    /// <returns>自身</returns>
    public MessageChainBuilder AddTextMessage(string text)
    {
        _messages.Add(new TextMessage
        {
            MessageData = new TextMessageData()
            {
                Text = text
            },
            MessageType = MessageDataType.Text
        });
        return this;
    }
    
    /// <summary>
    /// 添加艾特消息
    /// </summary>
    /// <param name="userId">qq号</param>
    /// <param name="isAll">是否at全体</param>
    /// <returns>自身</returns>
    public MessageChainBuilder AddMentionMessage(string userId,bool isAll = false)
    {
        _messages.Add(new AtMessage()
        {
            MessageData = new AtMessageData()
            {
                Qq = isAll? "all" : userId
            },
            MessageType = MessageDataType.At
        });
        return this;
    }
    
    /// <summary>
    /// 添加回复消息
    /// </summary>
    /// <param name="messageId">消息id</param>
    /// <returns>自身</returns>
    public MessageChainBuilder AddReplyMessage(string messageId)
    {
        _messages.Add(new ReplyMessage()
        {
            MessageData = new ReplyMessageData()
            {
                Id = messageId
            },
            MessageType = MessageDataType.Reply
        });
        return this;
    }

    /// <summary>
    /// 使用路径添加图片信息
    /// </summary>
    /// <param name="imageUrl">可输入服务器本地路径/网络路径/base64图片编码</param>
    /// <param name="isBase64">是否为base64，需添加 base64:// 前缀</param>
    /// <returns>自身</returns>
    public MessageChainBuilder AddImageMessage(string imageUrl,bool isBase64 = false)
    {
        if (string.IsNullOrWhiteSpace(imageUrl)) return this;
        var fileField = isBase64 && !imageUrl.StartsWith("base64://", StringComparison.OrdinalIgnoreCase)
            ? "base64://" + imageUrl
            : imageUrl;
        _messages.Add(new ImageMessage()
        {
            MessageData = new ImageMessageData()
            {
                File = fileField
            },
            MessageType = MessageDataType.Image
        });
        return this;
    }

    /// <summary>
    /// 使用文件流添加图片信息
    /// </summary>
    /// <param name="fs">文件流</param>
    /// <returns>自身</returns>
    public MessageChainBuilder AddImageMessage(FileStream fs)
    {
        if (fs == null) throw new ArgumentNullException(nameof(fs));
        if (fs.CanSeek) fs.Seek(0, SeekOrigin.Begin);
        
        using var ms = new MemoryStream();
        fs.CopyTo(ms);
        var bytes = ms.ToArray();
        if (bytes.Length == 0) return this;
        var base64 = Convert.ToBase64String(bytes);
        
        _messages.Add(new ImageMessage()
        {
            MessageData = new ImageMessageData()
            {
                File = "base64://" + base64
            },
            MessageType = MessageDataType.Image
        });
        return this;
    }

    public MessageChainBuilder AddVoiceMessage(string voiceUrl)
    {
        if (string.IsNullOrWhiteSpace(voiceUrl)) return this;
        _messages.Add(new RecordMessage()
        {
            MessageData = new RecordMessageData()
            {
                File = voiceUrl
            },
            MessageType = MessageDataType.Record
        });
        return this;
    }
}