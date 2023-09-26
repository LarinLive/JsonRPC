using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

public record OpenRpc(string Name, string? Last);


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

/// <summary>
/// Adds metadata to a single tag that is used by the <see cref="MethodObject"/>. It is not mandatory to have a <see cref="TagObject"/> per tag defined in the <see cref="MethodObject"/> instances.
/// </summary>
public record class TagObject 
{
	/// <summary>
	/// REQUIRED. The name of the tag.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// A short summary of the tag.
	/// </summary>
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// A verbose explanation of the target documentation. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Descripton { get; init; }

	/// <summary>
	/// Additional external documentation for this tag.
	/// </summary>
	[JsonPropertyName("externalDocs")]
	public ExternalDocumentationObject? ExternalDocs { get; init; }
}

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