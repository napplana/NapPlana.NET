using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class JsonMessageData : MessageDataBase
{
    // Raw JSON payload string
    [JsonPropertyName("data")] public string DataContent { get; set; } = string.Empty;
}

public class JsonMessage : Message
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Json;
    public override MessageDataBase MessageData { get; set;} = new JsonMessageData();
}

