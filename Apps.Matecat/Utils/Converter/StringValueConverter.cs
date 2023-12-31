﻿using System.Reflection;
using Newtonsoft.Json;

namespace Apps.Matecat.Utils.Converter;

public class StringValueConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => true;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        writer.WriteStartObject();

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(value);
            
            writer.WritePropertyName(GetJsonPropertyName(property));
            writer.WriteValue(propertyValue?.ToString());
        }

        writer.WriteEndObject();
    }

    private string GetJsonPropertyName(PropertyInfo property)
    {
        var jsonPropertyName = property.GetCustomAttribute<JsonPropertyAttribute>();
        return jsonPropertyName?.PropertyName ?? property.Name;
    }
}