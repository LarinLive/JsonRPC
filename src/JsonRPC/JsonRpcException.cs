using Json.Schema;
using System;

namespace Larin.JsonRPC;

/// <summary>
/// JSON-RPC parsing exceptions.
/// </summary>
public class JsonRpcException : ApplicationException
{
	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcException"/> class with the specified message
	/// </summary>
	/// <param name="message">An error message</param>
	public JsonRpcException(string? message)
		: base(message) { }

	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcException"/> class with the specified message and inner exception
	/// </summary>
	/// <param name="message">An error message</param>
	/// <param name="innerException">An inner exception</param>
	public JsonRpcException(string? message, Exception? innerException)
		: base(message, innerException) { }

	//TODO: Refactor schema evaluation errors
	public EvaluationResults[] SchemaEvaluationResult { get; init; } = Array.Empty<EvaluationResults>();

	/// <summary>
	/// Creates an instance of the <see cref="JsonRpcException"/> class for a unsupported identifier type
	/// </summary>
	/// <returns></returns>
	public static JsonRpcException ThrowUnsupportedIdentifierType() => new("Only string and long types are supported for JSON-RPC identifiers.");

	/// <summary>
	/// Creates an instance of the <see cref="JsonRpcException"/> class for an empty packet
	/// </summary>
	/// <returns>A newly created object</returns>
	public static JsonRpcException ThrowPacketIsEmpty() => new("The JSON-RPC packet is empty.");
}
