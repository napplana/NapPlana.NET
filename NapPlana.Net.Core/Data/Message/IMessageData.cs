using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public interface IMessageData
{
    [JsonPropertyName("type")]
    public abstract MessageDataType MessageType { get; set; }
    
    public abstract MessageDataBase MessageData { get; set; }
}

/// <summary>
/// 所有Message中Data的基类
/// </summary>
[Serializable]
public class MessageDataBase
{
    
}