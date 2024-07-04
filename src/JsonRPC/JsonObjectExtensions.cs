using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="JsonObject"/> class
/// </summary>
public static class JsonObjectExtensions
{
	/// <summary>
	/// Adds a <see cref="string"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, string? value)
	{
		@object.Add(name, value);
		return @object;
	}

	/// <summary>
	/// Adds a <see cref="Guid"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, Guid? value)
	{
		@object.Add(name, value);
		return @object;
	}

	/// <summary>
	/// Adds a <see cref="int"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, int? value)
	{
		@object.Add(name, value);
		return @object;
	}

	/// <summary>
	/// Adds a <see cref="long"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, long? value)
	{
		@object.Add(name, value);
		return @object;
	}

	/// <summary>
	/// Adds a <see cref="decimal"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, decimal? value)
	{
		@object.Add(name, value);
		return @object;
	}

	/// <summary>
	/// Adds a <see cref="double"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, double? value)
	{
		@object.Add(name, value);
		return @object;
	}

	/// <summary>
	/// Adds a <see cref="JsonNode"/> property to the <see cref="JsonObject"/> object.
	/// </summary>
	/// <param name="object">A target object for adding the property.</param>
	/// <param name="name">A property name.</param>
	/// <param name="value">A property value.</param>
	/// <returns>The same input object.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static JsonObject AddProperty(this JsonObject @object, string name, JsonNode? value)
	{
		@object.Add(name, value);
		return @object;
	}
}
