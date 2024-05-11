using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extensions methods for the <see cref="JrpcError"/> class
/// </summary>
public static class JrpcErrorExtensions
{
	private static JsonObject SerializeException(Exception e, JrpExceptionSerializationOptions options)
	{
		var result = new JsonObject()
			.AddProperty("type", e.GetType().Name)
			.AddProperty("source", e.Source)
			.AddProperty("message", e.Message);
		if (options.IncludeStackTrace)
			result.AddProperty("stackTrace", e.StackTrace);
		if (e.Data is not null)
		{
			var data = new JsonObject();
			foreach (var key in e.Data.Keys.OfType<string>())
			{
				var value = e.Data[key];
				data.AddProperty(key, JsonSerializer.Serialize(value, options.JsonSerializerOptions ?? JsonSerializerOptions.Default));
			}
			result.AddProperty("data", data);
		}

		if (e.InnerException is not null)
			result.AddProperty("exception", SerializeException(e.InnerException, options));
		return result;
	}

	/// <summary>
	/// Injects an exception information in a JSON-RPC error.
	/// </summary>
	/// <param name="error">A JSON-RPC erorr object.</param>
	/// <param name="exception">An exception to serialize.</param>
	/// <param name="options">An error serialization options.</param>
	/// <returns>A newely created instance of <see cref="JrpcError"/> class with the given exception information.</returns>
	public static JrpcError WithExceptionData(this JrpcError error, Exception exception, JrpExceptionSerializationOptions options)
		=> error.CopyWithData(
			new JsonObject()
				.AddProperty("exception", SerializeException(exception, options ?? JrpExceptionSerializationOptions.Default)));
}
