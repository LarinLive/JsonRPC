using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// License information for the exposed API.
/// </summary>
public record class LicenseObject
{
	/// <summary>
	/// REQUIRED. The license name used for the API.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// A URL to the license used for the API. MUST be in the format of a URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }
}
