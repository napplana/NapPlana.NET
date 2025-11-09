using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class MFaceMessageData : MessageDataBase
{
    [JsonPropertyName("emoji_id")] public string EmojiId { get; set; } = string.Empty;
    [JsonPropertyName("emoji_package_id")] public string EmojiPackageId { get; set; } = string.Empty;
    [JsonPropertyName("key")] public string Key { get; set; } = string.Empty;
    [JsonPropertyName("summary")] public string? Summary { get; set; }
}

public class MFaceMessage : Message
{
    public override MessageDataType MessageType { get; set; } = MessageDataType.MFace;
    public override MessageDataBase MessageData { get; set;} = new MFaceMessageData();
}

