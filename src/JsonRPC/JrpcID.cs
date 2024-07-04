using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// The class for a JSON-RPC request identifier
/// </summary>
public readonly struct JrpcID<T>: IJrpcID, IEquatable<JrpcID<T>>, IEquatable<IJrpcID> where T : notnull 
{
	private static readonly HashSet<Type> _allowedTypes =
	[
		typeof(string), typeof(Guid), 
		typeof(long), typeof(int), typeof(short), typeof(sbyte),
		typeof(ulong), typeof(uint), typeof(ushort), typeof(byte),
		typeof(decimal)
	];

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcID{T}"/> class
	/// </summary>
	/// <param name="value">A value of the identifier</param>
	public JrpcID(T value)
	{
		if (_allowedTypes.Contains(typeof(T)))
			Value = value;
		else
			throw JrpcException.UnsupportedIdentifierType();
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
		decimal d => (JsonValue)d,
		_ => throw JrpcException.UnsupportedIdentifierType()
	};

	/// <inheritdoc/>
	public void WriteTo(Utf8JsonWriter writer)
	{
		switch (Value)
		{
			case string s: 
				writer.WriteStringValue(s);
				break;
			case Guid s:
				writer.WriteStringValue(s.ToString("D"));
				break;
			case long i64:
				writer.WriteNumberValue(i64);
				break;
			case int i32:
				writer.WriteNumberValue(i32);
				break;
			case short i16:
				writer.WriteNumberValue(i16);
				break;
			case sbyte i8:
				writer.WriteNumberValue(i8);
				break;
			case ulong u64:
				writer.WriteNumberValue(u64);
				break;
			case uint u32:
				writer.WriteNumberValue(u32);
				break;
			case ushort u16:
				writer.WriteNumberValue(u16);
				break;
			case byte u8:
				writer.WriteNumberValue(u8);
				break;
			case decimal d:
				writer.WriteNumberValue(d);
				break;
			default:
				throw JrpcException.UnsupportedIdentifierType();
		}
	}

	/// <inheritdoc/>
	public bool Equals(JrpcID<T> other) => Value.Equals(other.Value);

	/// <inheritdoc/>
	public bool Equals(IJrpcID? other)
	{
		if (other is JrpcID<T> b)
			return Value.Equals(b.Value);
		else
			return false;
	}

	/// <inheritdoc/>
	public static bool operator ==(JrpcID<T> left, JrpcID<T> right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(JrpcID<T> left, JrpcID<T> right) => !(left == right);
}
