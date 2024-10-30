using Newtonsoft.Json;

namespace Apps.Matecat.Utils.Converter;

public class UnixDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        if (reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.String)
            throw new JsonSerializationException(
                $"Unexpected token parsing date. Expected Integer or String, got {reader.TokenType}.");

        long unixTime;
        if (reader.TokenType == JsonToken.Integer)
        {
            unixTime = (long)reader.Value!;
        }
        else 
        {
            if (!long.TryParse((string)reader.Value!, out unixTime))
                throw new JsonSerializationException($"Invalid Unix timestamp: {reader.Value}");
        }

        return DateTimeOffset.FromUnixTimeSeconds(unixTime).UtcDateTime;
    }

    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            var unixTime = new DateTimeOffset(value.Value).ToUnixTimeSeconds();
            writer.WriteValue(unixTime);
        }
        else
        {
            writer.WriteNull();
        }
    }
}