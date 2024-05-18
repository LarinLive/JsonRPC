using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extensions methods for the <see cref="JrpcError"/> class
/// </summary>
public static class JrpcErrorExtensions
{
	public static JsonObject ToJsonObject(this Exception e, JrpcExceptionSerializationOptions options)
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
			var ms = new MemoryStream();
			foreach (var key in e.Data.Keys.OfType<string>())
			{
				var value = e.Data[key];
				using (var writer = new Utf8JsonWriter(ms))
					JsonSerializer.Serialize(writer, value, options.JsonSerializerOptions ?? JsonSerializerOptions.Default);
				ms.Position = 0;
				data.AddProperty(key, JsonNode.Parse(ms));
			}
			result.AddProperty("data", data);
		}

		if (options.IncludeInnerExceptions && e.InnerException is not null)
			result.AddProperty("exception", e.InnerException.ToJsonObject(options));
		return result;
	}

	/// <summary>
	/// Injects an exception information in a JSON-RPC error.
	/// </summary>
	/// <param name="error">A JSON-RPC erorr object.</param>
	/// <param name="exception">An exception to serialize.</param>
	/// <param name="options">An error serialization options.</param>
	/// <returns>A newely created instance of <see cref="JrpcError"/> class with the given exception information.</returns>
	public static JrpcError WithExceptionData(this JrpcError error, Exception exception, JrpcExceptionSerializationOptions? options)
		=> error.CopyWithData(
			new JsonObject()
				.AddProperty("exception", exception.ToJsonObject(options ?? JrpcExceptionSerializationOptions.Default)));
}
