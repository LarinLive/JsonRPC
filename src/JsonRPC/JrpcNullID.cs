using System;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace LarinLive.JsonRPC;

/// <summary>
/// The class for a JSON-RPC null identifier
/// </summary>
public readonly struct JrpcNullID : IJrpcID, IEquatable<JrpcNullID>, IEquatable<IJrpcID>
{
	/// <summary>
	/// Creates a new instance of the <see cref="JrpcNullID"/> class
	/// </summary>
	public JrpcNullID() { }

	/// <inheritdoc/>
	public override bool Equals(object? obj) => obj is JrpcNullID;

	/// <inheritdoc/>
	public override int GetHashCode() => 0;

	/// <inheritdoc/>
	public override string ToString() => "null";

	/// <inheritdoc/>
	public JsonValue? ToJsonValue() => JsonValue.Create(JsonValueKind.Null);

	/// <inheritdoc/>
	public void WriteTo(Utf8JsonWriter writer)
	{
		writer.WriteNullValue();
	}

	/// <inheritdoc/>
	public bool Equals(JrpcNullID other) => true;

	/// <inheritdoc/>
	public bool Equals(IJrpcID? other) => other is JrpcNullID;

	/// <inheritdoc/>
	public static bool operator ==(JrpcNullID left, JrpcNullID right) => true;

	/// <inheritdoc/>
	public static bool operator !=(JrpcNullID left, JrpcNullID right) => false;
}
