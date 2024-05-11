using System;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extensions methods for the <see cref="JrpcError"/> class
/// </summary>
public static class JrpcErrorExtensions
{
	private static JsonObject SerializeException(Exception e, bool includeStackTrace = false)
	{
		var result = new JsonObject()
			.AddProperty("type", e.GetType().Name)
			.AddProperty("message", e.Message)
			.AddProperty("source", e.Source);
		if (includeStackTrace)
			result.AddProperty("stackTrace", e.StackTrace);
		if (e.InnerException is not null)
			result.AddProperty("exception", SerializeException(e.InnerException, includeStackTrace));
		return result;
	}

	/// <summary>
	/// Injects an exception information in a JSON-RPC error
	/// </summary>
	/// <param name="error">A JSON-RPC erorr object</param>
	/// <param name="exception">An exception</param>
	/// <param name="includeStackTrace"></param>
	/// <returns>A newely created instance of <see cref="JrpcError"/> class with the given exception information.</returns>
	public static JrpcError WithExceptionData(JrpcError error, Exception exception, bool includeStackTrace = false)
		=> error.CopyWithData(
			new JsonObject()
				.AddProperty("exception", SerializeException(exception, includeStackTrace)));
}
