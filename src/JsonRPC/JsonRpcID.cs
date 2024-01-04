using System.IO;
using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

/// <summary>
/// The base class for a JSON RPC-request identifier
/// </summary>
public abstract class JsonRpcID
{
	/// <summary>
	/// Serializes
	/// </summary>
	/// <returns></returns>
	protected internal abstract JsonValue ToJsonValue();
}

/// <summary>
/// The class for a JSON RPC-request identifier
/// </summary>
public sealed class JsonRpcID<T> : JsonRpcID where T : notnull
{

	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcID{T}"/> class
	/// </summary>
	/// <param name="value">A value of the identifier</param>
	public JsonRpcID(T value)
	{
		Value = value;
	}

	/// <summary>
	/// The value of the identifier
	/// </summary>
	public T Value { get; }

	/// <inheritdoc/>
	public override string? ToString() => 
		Value.ToString();

	/// <inheritdoc/>
	public override int GetHashCode() =>
		Value.GetHashCode();

	/// <inheritdoc/>
	public override bool Equals(object? obj) 
	{
		if (obj is JsonRpcID<T> b)
			return Value.Equals(b.Value);
		else
			return false;
	}


	/// <inheritdoc/>
	protected internal override JsonValue ToJsonValue() => Value switch
	{
		string s => (JsonValue)s!,
		long i64 => (JsonValue)i64,
		int i32 => (JsonValue)i32,
		_ => throw JsonRpcException.ThrowUnsupportedIdentifierType()
	};
}
