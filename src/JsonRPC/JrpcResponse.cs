using System;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// A JSON-RPC response struct.
/// </summary>
public readonly record struct JrpcResponse : IJrpcObject
{
	private readonly JrpcError _error = default;

	/// <summary>
	/// Initializes a new instance of the <see cref="JrpcResponse"/> struct for a successful result.
	/// </summary>
	/// <param name="result">An optional result data.</param>
	/// <param name="id">A JSON-RPC request identifier.</param>
	public JrpcResponse(JsonNode? result, IJrpcID id)
	{
		IsSuccess = true;
		Result = result;
		ID = id;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="JrpcResponse"/> struct for an error.
	/// </summary>
	/// <param name="error">A JSON-RPC error details.</param>
	/// <param name="id">A JSON-RPC request identifier.</param>
	public JrpcResponse(JrpcError error, IJrpcID id)
	{
		IsSuccess = false;
		_error = error;
		ID = id;
	}

	/// <summary>
	/// The JSON-RPC request identifier.
	/// </summary>
	public IJrpcID ID { get; }

	/// <summary>
	/// The flag of the success result.
	/// </summary>
	public bool IsSuccess { get; }

	/// <summary>
	/// The optional result data.
	/// </summary>
	public JsonNode? Result { get; }

	/// <summary>
	/// The error details. This information is available only for unsuccessful responses.
	/// </summary>
	public JrpcError Error => !IsSuccess ? _error: throw new InvalidOperationException("Error is unavaiable for a successful response.");

	/// <summary>
	/// A type of the JSON-RPC object.
	/// </summary>
	public JrpcObjectType Type => JrpcObjectType.Response; 
}

