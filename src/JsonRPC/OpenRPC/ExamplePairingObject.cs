using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
///	The Example Pairing object consists of a set of example params and result. The result is what you can expect from the JSON-RPC service given the exact params.
/// </summary>
public record class ExamplePairingObject
{
	/// <summary>
	/// REQUIRED Name for the example pairing.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// A verbose explanation of the example pairing.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// Short description for the example pairing.
	/// </summary>
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// REQUIRED Example parameters.
	/// </summary>
	[JsonPropertyName("params")]
	public ExampleObject[]? Params { get; init; }

	/// <summary>
	/// Example result. When undefined, the example pairing represents usage of the method as a notification.
	/// </summary>
	[JsonPropertyName("result")]
	public ExampleObject? Result { get; init; }
}
