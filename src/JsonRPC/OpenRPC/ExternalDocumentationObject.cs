using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// Allows referencing an external resource for extended documentation.
/// </summary>
public record class ExternalDocumentationObject
{
	/// <summary>
	/// A verbose explanation of the target documentation. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Descripton { get; init; }

	/// <summary>
	/// REQUIRED. The URL for the target documentation. Value MUST be in the format of a URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }
}