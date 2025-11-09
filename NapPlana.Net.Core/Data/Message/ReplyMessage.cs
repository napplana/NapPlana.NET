using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class ReplyMessageData : MessageDataBase
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

public class ReplyMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Reply;
    public MessageDataBase MessageData { get;set; } = new ReplyMessageData();
}

