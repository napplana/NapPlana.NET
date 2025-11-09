using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class ContactMessageData : MessageDataBase
{
    // qq or group
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
}

public class ContactMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Contact;
    public MessageDataBase MessageData { get;set; } = new ContactMessageData();
}

