using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class DiceMessageData : MessageDataBase
{
    [JsonPropertyName("result")] public string? Result { get; set; }
}

public class DiceMessage : Message
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Dice;
    public override MessageDataBase MessageData { get; set;} = new DiceMessageData();
}

