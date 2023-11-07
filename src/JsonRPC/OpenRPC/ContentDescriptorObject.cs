using Json.Schema;
using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// Content Descriptors are objects that do just as they suggest - describe content. They are reusable ways of describing either parameters or result. They MUST have a schema.
/// </summary>
public record class ContentDescriptorObject
{
	/// <summary>
	/// REQUIRED. Name of the content that is being described. If the content described is a method parameter assignable by-name, this field SHALL define the parameter’s key (ie name).
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// A short summary of the content that is being described.
	/// </summary>
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// A verbose explanation of the content descriptor behavior. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// Determines if the content is a required field. Default value is false.
	/// </summary>
	[JsonPropertyName("required")]
	public bool Required { get; init; }

	/// <summary>
	/// REQUIRED. Schema that describes the content.
	/// </summary>
	[JsonPropertyName("schema")]
	public JsonSchema? Schema { get; init; } 

	/// <summary>
	/// Specifies that the content is deprecated and SHOULD be transitioned out of usage. Default value is false.
	/// </summary>
	[JsonPropertyName("deprecated")]
	public bool Deprecated { get; init; }
}
