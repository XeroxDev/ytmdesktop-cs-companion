using System.Text.Json;
using System.Text.Json.Serialization;
using NuGet.Versioning;

namespace XeroxDev.YTMDesktop.Companion.Converter;

/// <summary>
/// This class is used to convert a <see cref="SemanticVersion"/> to and from a JSON string.
/// </summary>
public class SemanticVersionJsonConverter : JsonConverter<SemanticVersion>
{
    public override SemanticVersion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            if (reader.TokenType != JsonTokenType.String) return null;
            if (reader.GetString() is null) return null;
            if (!SemanticVersion.TryParse(reader.GetString()!, out var version)) return null;
        }
        catch (Exception)
        {
            return null;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, SemanticVersion value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}