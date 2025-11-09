using System.Text.Json.Serialization;

namespace NapPlana.Core.Data.Message;

public class MusicMessageData : MessageDataBase
{
    // Type: qq / 163 / kugou / migu / kuwo / custom
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    // For existing music sources
    [JsonPropertyName("id")] public string? Id { get; set; }

    // For custom music
    [JsonPropertyName("url")] public string? Url { get; set; }
    [JsonPropertyName("audio")] public string? Audio { get; set; }
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("image")] public string? Image { get; set; }
    [JsonPropertyName("singer")] public string? Singer { get; set; }
}

public class MusicMessage : IMessageData
{
    public MessageDataType MessageType { get; set; } = MessageDataType.Music;
    public MessageDataBase MessageData { get;set; } = new MusicMessageData();
}

