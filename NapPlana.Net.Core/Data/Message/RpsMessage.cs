using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class RpsMessageData : MessageDataBase
{
    // Receive only: result (e.g., rock/paper/scissors outcome)
    [JsonPropertyName("result")] public string? Result { get; set; }
}

public class RpsMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Rps;
    public MessageDataBase MessageData { get; set;} = new RpsMessageData();
}

