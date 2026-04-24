using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Json
{
    public class GuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return Guid.Empty;
                }

                if (Guid.TryParse(stringValue, out var guid))
                {
                    return guid;
                }

                throw new JsonException($"Invalid GUID format: {stringValue}");
            }

            throw new JsonException($"Unexpected token {reader.TokenType} when parsing GUID");
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
