using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

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
