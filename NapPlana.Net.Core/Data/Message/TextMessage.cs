using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class TextMessageData: MessageDataBase
{
    [JsonPropertyName("text")]
    public String Text { get; set; } = String.Empty;
}

public class TextMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Text;
    public MessageDataBase MessageData { get;set; } = new TextMessageData();
}