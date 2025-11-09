using System.Text.Json.Serialization;
using System.Text.Json;

namespace NapPlana.Core.Data.Action;

[Serializable]
public class ActionResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("retcode")]
    public int RetCode { get; set; } = 0;
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("data")]
    public object Data { get; set; } = new { };
    [JsonPropertyName("wording")]
    public string Wording { get; set; } = string.Empty;
    [JsonPropertyName("echo")]
    public string Echo { get; set; } = string.Empty;
    
    public T? GetData<T>()
    {
        if (Data is null)
        {
            return default;
        }
        if (Data is T t)
        {
            return t;
        }
        if (Data is JsonElement je)
        {
            try
            {
                return je.Deserialize<T>();
            }
            catch
            {
                // fall through to generic convert
            }
        }
        try
        {
            var json = JsonSerializer.Serialize(Data);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }
}