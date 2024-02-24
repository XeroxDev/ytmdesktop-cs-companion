using System.Text.Json.Serialization;
using XeroxDev.YTMDesktop.Companion.Constants;

namespace XeroxDev.YTMDesktop.Companion.Models.Output;

/// <summary>
/// This class is the output from the <see cref="Endpoints.Metadata"/> endpoint.
/// </summary>
public class MetadataOutput
{
    /// <summary>
    /// All available API version.
    /// </summary>
    [JsonPropertyName("apiVersions"), JsonRequired]
    public List<string> ApiVersions { get; set; } = null!;
}