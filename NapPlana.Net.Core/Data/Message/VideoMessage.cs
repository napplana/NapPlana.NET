using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class VideoMessageData : MessageDataBase
{
    [JsonPropertyName("file")] public string File { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("thumb")] public string? Thumb { get; set; }
    [JsonPropertyName("url")] public string? Url { get; set; }
    [JsonPropertyName("path")] public string? Path { get; set; }
    [JsonPropertyName("file_id")] public string? FileId { get; set; }
    [JsonPropertyName("file_size")] public string? FileSize { get; set; }
    [JsonPropertyName("file_unique")] public string? FileUnique { get; set; }
}

public class VideoMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Video;
    public MessageDataBase MessageData { get;set; } = new VideoMessageData();
}

