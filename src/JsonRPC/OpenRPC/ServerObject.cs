using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// An object representing a Server.
/// </summary>
public record class ServerObject
{
	/// <summary>
	/// REQUIRED. A name to be used as the cannonical name for the server.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// REQUIRED. The default value to use for substitution, which SHALL be sent if an alternate value is not supplied. 
	/// Note this behavior is different than the Schema Object’s treatment of default values, because in those cases parameter values are optional.
	/// </summary>
	[JsonPropertyName("default")]
	public string Default { get; init; } = string.Empty;

	/// <summary>
	/// An optional description for the server variable. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }
}
