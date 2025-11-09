using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class Message
{
    [JsonPropertyName("type")]
    public virtual MessageDataType MessageType { get; set; }
    
    public virtual MessageDataBase MessageData { get; set; }
}

/// <summary>
/// 所有Message中Data的基类
/// </summary>
[Serializable]
public class MessageDataBase
{
    
}