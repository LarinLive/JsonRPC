using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

/// <summary>
/// The class for a JSON RPC-request identifier
/// </summary>
public readonly struct JrpcID<T>: IJrpcID, IEquatable<JrpcID<T>>, IEquatable<IJrpcID> where T : notnull 
{
	private static readonly IReadOnlySet<Type> _allowedTypes = new HashSet<Type>()
	{
		typeof(string), typeof(Guid), 
		typeof(long), typeof(int), typeof(short), typeof(sbyte),
		typeof(ulong), typeof(uint), typeof(ushort), typeof(byte) 
	};

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcID{T}"/> class
	/// </summary>
	/// <param name="value">A value of the identifier</param>
	public JrpcID(T value)
	{
		if (_allowedTypes.Contains(typeof(T)))
			Value = value;
		else
			throw JrpcException.CreateUnsupportedIdentifierType();
	}

	/// <summary>
	/// The value of the identifier
	/// </summary>
	public T Value { get; }

	/// <inheritdoc/>
	public override string? ToString() => Value.ToString();

	/// <inheritdoc/>
	public override int GetHashCode() => Value.GetHashCode();

	/// <inheritdoc/>
	public override bool Equals(object? obj) 
	{
		if (obj is JrpcID<T> b)
			return Value.Equals(b.Value);
		else
			return false;
	}

	/// <inheritdoc/>
	public JsonValue ToJsonValue() => Value switch
	{
		string s => (JsonValue)s,
		Guid s => (JsonValue)s.ToString("D"),
		long i64 => (JsonValue)i64,
		int i32 => (JsonValue)i32,
		short i16 => (JsonValue)i16,
		sbyte i8 => (JsonValue)i8,
		ulong u64 => (JsonValue)u64,
		uint u32 => (JsonValue)u32,
		ushort u16 => (JsonValue)u16,
		byte u8 => (JsonValue)u8,
		_ => throw JrpcException.CreateUnsupportedIdentifierType()
	};

	/// <inheritdoc/>
	public bool Equals(JrpcID<T> other) => Value.Equals(other.Value);

	/// <inheritdoc/>
	public bool Equals(IJrpcID? other)
	{
		if (other is null)
			return false;
		else if (GetType() == other.GetType())
			return Value.Equals(((JrpcID<T>)other).Value);
		else
			return false;
	}

	public static bool operator ==(JrpcID<T> left, JrpcID<T> right) => left.Equals(right);

	public static bool operator !=(JrpcID<T> left, JrpcID<T> right) => !left.Equals(right);
}
