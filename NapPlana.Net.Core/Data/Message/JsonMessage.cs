using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class JsonMessageData : MessageDataBase
{
    // Raw JSON payload string
    [JsonPropertyName("data")] public string DataContent { get; set; } = string.Empty;
}

public class JsonMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Json;
    public MessageDataBase MessageData { get; set;} = new JsonMessageData();
}

