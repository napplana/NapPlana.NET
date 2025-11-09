using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class ImageMessageData : MessageDataBase
{
    // Common / send optional fields
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("summary")] public string? Summary { get; set; }
    // file: normal file path or "marketface" for market face
    [JsonPropertyName("file")] public string File { get; set; } = string.Empty;
    // Optional sub type (e.g., flash?)
    [JsonPropertyName("sub_type")] public string? SubType { get; set; }

    // Receive-only fields (may also appear after upload)
    [JsonPropertyName("file_id")] public string? FileId { get; set; }
    [JsonPropertyName("url")] public string? Url { get; set; }
    [JsonPropertyName("path")] public string? Path { get; set; }
    [JsonPropertyName("file_size")] public string? FileSize { get; set; }
    [JsonPropertyName("file_unique")] public string? FileUnique { get; set; }
}

public class ImageMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Image;
    public MessageDataBase MessageData { get;set; } = new ImageMessageData();
}

