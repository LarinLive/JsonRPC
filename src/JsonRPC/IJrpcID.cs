using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

/// <summary>
/// Encapsulates a JSON-RPC request identifier.
/// </summary>
public interface IJrpcID : IEquatable<IJrpcID>
{
	/// <summary>
	/// Serializes a JSON-RPC request identifier to a <see cref="JsonValue"/> object.
	/// </summary>
	/// <returns>An instance of the <see cref="JsonValue"/> class that holds the ID.</returns>
	JsonValue ToJsonValue();

	/// <summary>
	/// Writes a JSON-RPC request identifier to a <see cref="Utf8JsonWriter"/> object .
	/// </summary>
	/// <param name="writer">A JSON text writer.</param>
	void WriteTo(Utf8JsonWriter writer);
}
