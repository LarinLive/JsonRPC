using System.Text.Json;

namespace LarinLive.JsonRPC;

/// <summary>
/// A JSON-RPC exception serialization options.
/// </summary>
public class JrpcExceptionSerializationOptions
{
	/// <summary>
	/// An instance of the <see cref="JrpcExceptionSerializationOptions"/> with the default values.
	/// </summary>
	public static readonly JrpcExceptionSerializationOptions Default = new();

	/// <summary>
	/// Include inner exceptions in the output
	/// </summary>
	public bool IncludeInnerExceptions { get; init; } = true;

	/// <summary>
	/// Include a stack trace in the output
	/// </summary>
	public bool IncludeStackTrace { get; init; } = false;

	/// <summary>
	/// Output serialization options.
	/// </summary>
	public JsonSerializerOptions? JsonSerializerOptions { get; init; } 
}
