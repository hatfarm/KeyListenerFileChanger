// DictionaryKeysJsonConverter.cs
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace GlobalKeyListener.Serialization;

public class DictionaryKeysJsonConverter : JsonConverter<Dictionary<Keys, string>>
{
    public override Dictionary<Keys, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dictionary = new Dictionary<Keys, string>();

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return dictionary;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            reader.Read();
            string value = reader.GetString();

            if (Enum.TryParse(propertyName, out Keys key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                throw new JsonException($"Unable to convert \"{propertyName}\" to {nameof(Keys)}.");
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<Keys, string> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value)
        {
            writer.WriteString(kvp.Key.ToString(), kvp.Value);
        }

        writer.WriteEndObject();
    }
}
