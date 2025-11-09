using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class RpsMessageData : MessageDataBase
{
    // Receive only: result (e.g., rock/paper/scissors outcome)
    [JsonPropertyName("result")] public string? Result { get; set; }
}

public class RpsMessage : Message
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Rps;
    public override MessageDataBase MessageData { get; set;} = new RpsMessageData();
}

