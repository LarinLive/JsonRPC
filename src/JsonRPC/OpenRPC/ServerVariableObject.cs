using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// An object representing a Server Variable for server URL template substitution.
/// </summary>
public record class ServerVariableObject
{
	/// <summary>
	/// An enumeration of string values to be used if the substitution options are from a limited set.
	/// </summary>
	[JsonPropertyName("enum")]
	public string[]? Enum { get; init; }

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
