using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// Represents a possible design-time link for a result. The presence of a link does not guarantee the caller’s ability to successfully invoke it, rather it provides a known relationship and traversal mechanism between results and other methods.
/// </summary>
public record class LinkObject
{
	/// <summary>
	/// REQUIRED. Cannonical name of the link.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// A description of the link. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// Short description for the link.
	/// </summary>
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// The name of an existing, resolvable OpenRPC method, as defined with a unique method. This field MUST resolve to a unique Method Object. As opposed to Open Api, Relative method values ARE NOT permitted.
	/// </summary>
	[JsonPropertyName("method")]
	public string? Method { get; init; }

	/// <summary>
	/// Additional external documentation for this tag.
	/// </summary>
	[JsonPropertyName("params")]
	public Dictionary<string, object?> Params { get; } = new();

	/// <summary>
	/// A server object to be used by the target method.
	/// </summary>
	[JsonPropertyName("server")]
	public ServerObject? Server { get; init; }
}
