using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="JsonObject"/> class
/// </summary>
public static class JsonObjectExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, string? value)
	{
		@object.Add(name, value);
		return @object;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, Guid? value)
	{
		@object.Add(name, value);
		return @object;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, int? value)
	{
		@object.Add(name, value);
		return @object;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, long? value)
	{
		@object.Add(name, value);
		return @object;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, decimal? value)
	{
		@object.Add(name, value);
		return @object;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, double? value)
	{
		@object.Add(name, value);
		return @object;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, JsonNode? value)
	{
		@object.Add(name, value);
		return @object;
	}
}
