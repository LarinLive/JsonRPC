using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

/// <summary>
/// A JSON RPC-request identifier
/// </summary>
public interface IJsonRpcID
{
	/// <summary>
	/// Serializes a JSON-RPC request identifier to an object of the <see cref="JsonValue"/> class.
	/// </summary>
	/// <returns></returns>
	JsonValue ToJsonValue();
}

/// <summary>
/// The class for a JSON RPC-request identifier
/// </summary>
public readonly struct JsonRpcID<T>: IJsonRpcID, IEquatable<JsonRpcID<T>>, IEquatable<IJsonRpcID> where T : notnull 
{
	private static readonly IReadOnlySet<Type> _allowedTypes = new HashSet<Type>()
	{
		typeof(string), typeof(Guid), 
		typeof(long), typeof(int), typeof(short), typeof(sbyte),
		typeof(ulong), typeof(uint), typeof(ushort), typeof(byte) 
	};

	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcID{T}"/> class
	/// </summary>
	/// <param name="value">A value of the identifier</param>
	public JsonRpcID(T value)
	{
		if (_allowedTypes.Contains(typeof(T)))
			Value = value;
		else
			throw JsonRpcException.ThrowUnsupportedIdentifierType();
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
	public JsonValue ToJsonValue() => Value switch
	{
		string s => (JsonValue)s!,
		Guid s => (JsonValue)s.ToString("D"),
		long i64 => (JsonValue)i64,
		int i32 => (JsonValue)i32,
		short i16 => (JsonValue)i16,
		sbyte i8 => (JsonValue)i8,
		ulong u64 => (JsonValue)u64,
		uint u32 => (JsonValue)u32,
		ushort u16 => (JsonValue)u16,
		byte u8 => (JsonValue)u8,
		_ => throw JsonRpcException.ThrowUnsupportedIdentifierType()
	};


	/// <inheritdoc/>
	public bool Equals(JsonRpcID<T> other) => Value.Equals(other.Value);

	/// <inheritdoc/>
	public bool Equals(IJsonRpcID? other)
	{
		if (other is null)
			return false;
		else if (GetType() == other.GetType())
			return Value.Equals(((JsonRpcID<T>)other).Value);
		else
			return false;
	}
}
