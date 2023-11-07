using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
///	The Example object is an object that defines an example that is intended to match the schema of a given Content Descriptor.
/// </summary>
public record class ExampleObject
{
	/// <summary>
	/// Cannonical name of the example.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	///	A verbose explanation of the example. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// Short description for the example.
	/// </summary>
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// Embedded literal example. The value field and externalValue field are mutually exclusive. 
	/// To represent examples of media types that cannot naturally represented in JSON, use a string value to contain the example, escaping where necessary.
	/// </summary>
	[JsonPropertyName("value")]
	public object? Value { get; init; }

	/// <summary>
	/// A URL that points to the literal example. This provides the capability to reference examples that cannot easily be included in JSON documents. 
	/// The value field and externalValue field are mutually exclusive.
	/// </summary>
	[JsonPropertyName("externalValue")]
	public string? ExternalValue { get; init; }
}
