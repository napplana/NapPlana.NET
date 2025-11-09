using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class NodeMessageData : MessageDataBase
{
    // Existing message reference
    [JsonPropertyName("id")] public string? Id { get; set; }
    // Forged message fields
    [JsonPropertyName("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("nickname")] public string? Nickname { get; set; }
    // Content: list of segments/messages
    [JsonPropertyName("content")] public List<MessageDataBase>? Content { get; set; }
}

public class NodeMessage : Message
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.Node;
    public override MessageDataBase MessageData { get; set;} = new NodeMessageData();
}

