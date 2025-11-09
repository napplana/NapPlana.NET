using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NapPlana.Core.Utilities;

public class SafeJsonStringEnumConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(SafeEnumConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private class SafeEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private static readonly Dictionary<T, string> NameByValue;
        private static readonly Dictionary<string, T> ValueByName;
        private static readonly T? NoneValue;

        static SafeEnumConverter()
        {
            NameByValue = new Dictionary<T, string>();
            ValueByName = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

            var type = typeof(T);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var enumValue = (T)field.GetValue(null)!;

                // prefer JsonPropertyName on enum member; fallback to EnumMember(Value) ; else use name
                var jsonName = field.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
                if (string.IsNullOrEmpty(jsonName))
                {
                    var enumMember = field.GetCustomAttribute<System.Runtime.Serialization.EnumMemberAttribute>();
                    if (!string.IsNullOrEmpty(enumMember?.Value))
                    {
                        jsonName = enumMember!.Value!;
                    }
                }
                jsonName ??= field.Name;

                NameByValue[enumValue] = jsonName;
                if (!ValueByName.ContainsKey(jsonName))
                {
                    ValueByName[jsonName] = enumValue;
                }
            }

            if (Enum.TryParse<T>("None", out var none))
            {
                NoneValue = none;
            }
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.Null || 
                    (reader.TokenType == JsonTokenType.String && string.IsNullOrEmpty(reader.GetString())))
                {
                    if (NoneValue.HasValue)
                    {
                        return NoneValue.Value;
                    }
                    return default;
                }

                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString()!;
                    if (ValueByName.TryGetValue(s, out var mapped))
                    {
                        return mapped;
                    }

                    // fallback: try parse enum names
                    if (Enum.TryParse<T>(s, ignoreCase: true, out var parsed))
                    {
                        return parsed;
                    }

                    // unknown -> None or default
                    if (NoneValue.HasValue)
                    {
                        return NoneValue.Value;
                    }
                    return default;
                }

                // unexpected token -> try to parse as number
                if (reader.TokenType == JsonTokenType.Number)
                {
                    if (reader.TryGetInt32(out var intVal))
                    {
                        var obj = (T)Enum.ToObject(typeof(T), intVal);
                        return obj;
                    }
                }

                // fallback
                if (NoneValue.HasValue)
                {
                    return NoneValue.Value;
                }
                return default;
            }
            catch (JsonException)
            {
                if (NoneValue.HasValue)
                {
                    return NoneValue.Value;
                }
                return default;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // None -> write null
            if (NoneValue.HasValue && EqualityComparer<T>.Default.Equals(value, NoneValue.Value))
            {
                writer.WriteNullValue();
                return;
            }

            if (NameByValue.TryGetValue(value, out var name))
            {
                writer.WriteStringValue(name);
                return;
            }

            // fallback to enum name
            writer.WriteStringValue(value.ToString());
        }
    }
}
