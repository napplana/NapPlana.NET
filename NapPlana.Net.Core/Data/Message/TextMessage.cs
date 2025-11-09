using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class TextMessageData: MessageDataBase
{
    [JsonPropertyName("text")]
    public String Text { get; set; } = String.Empty;
}

public class TextMessage : Message
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Text;
    public override MessageDataBase MessageData { get;set; } = new TextMessageData();
}