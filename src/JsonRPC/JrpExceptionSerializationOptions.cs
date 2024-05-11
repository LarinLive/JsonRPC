using System.Text.Json;

namespace LarinLive.JsonRPC;

public class JrpExceptionSerializationOptions
{
	public static readonly JrpExceptionSerializationOptions Default = new();

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
