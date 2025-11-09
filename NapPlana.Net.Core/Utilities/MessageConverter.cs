using System.Text.Json;
using System.Text.Json.Serialization;

namespace NapPlana.Core.Utilities;

public class MessageConverter : JsonConverter<object>
{
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString() ?? string.Empty;
            case JsonTokenType.StartArray:
                var array = JsonSerializer.Deserialize<object[]>(ref reader, options);
                return array ?? Array.Empty<object>();
            default:
                throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        if (value is string str)
        {
            writer.WriteStringValue(str);
        }
        else if (value is Array array)
        {
            JsonSerializer.Serialize(writer, array, options);
        }
        else
        {
            throw new JsonException($"Unsupported message type: {value.GetType()}");
        }
    }
}