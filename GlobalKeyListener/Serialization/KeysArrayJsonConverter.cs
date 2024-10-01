// KeysArrayJsonConverter.cs
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace GlobalKeyListener.Serialization;

public class KeysArrayJsonConverter : JsonConverter<Keys[]>
{
    public override Keys[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException();
        }

        var keysList = new List<Keys>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return keysList.ToArray();
            }

            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string keyString = reader.GetString();
            if (Enum.TryParse(keyString, out Keys key))
            {
                keysList.Add(key);
            }
            else
            {
                throw new JsonException($"Unable to convert \"{keyString}\" to {nameof(Keys)}.");
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Keys[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var key in value)
        {
            writer.WriteStringValue(key.ToString());
        }

        writer.WriteEndArray();
    }
}
