using System;
using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="JsonObject"/> class
/// </summary>
internal static class JsonObjectExtensions
{
	public static JsonObject AddProperty(this JsonObject @object, string name, string? value)
	{
		@object.Add(name, value);
		return @object;
	}

	public static JsonObject AddProperty(this JsonObject @object, string name, Guid? value)
	{
		@object.Add(name, value);
		return @object;
	}

	public static JsonObject AddProperty(this JsonObject @object, string name, int? value)
	{
		@object.Add(name, value);
		return @object;
	}

	public static JsonObject AddProperty(this JsonObject @object, string name, long? value)
	{
		@object.Add(name, value);
		return @object;
	}

	public static JsonObject AddProperty(this JsonObject @object, string name, decimal? value)
	{
		@object.Add(name, value);
		return @object;
	}

	public static JsonObject AddProperty(this JsonObject @object, string name, double? value)
	{
		@object.Add(name, value);
		return @object;
	}

	public static JsonObject AddProperty(this JsonObject @object, string name, JsonNode? value)
	{
		@object.Add(name, value);
		return @object;
	}
}
