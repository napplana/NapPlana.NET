using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class DiceMessageData : MessageDataBase
{
    [JsonPropertyName("result")] public string? Result { get; set; }
}

public class DiceMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Dice;
    public MessageDataBase MessageData { get; set;} = new DiceMessageData();
}

