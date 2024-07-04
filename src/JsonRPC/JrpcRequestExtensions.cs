using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="JrpcRequest"/> class
/// </summary>
public static class JrpcRequestExtensions
{
	/// <summary>
	/// Creates a successful response for the given request object
	/// </summary>
	/// <param name="request">A JSON-RPC request</param>
	/// <param name="result">A result</param>
	/// <returns>A new instance of the <see cref="JrpcResponse"/> with the id of the given request</returns>
	public static JrpcResponse CreateResult(this JrpcRequest request, JsonNode result)
		=> new(result, request.ID ?? throw JrpcException.CannotCreateResponseForNotification());

	/// <summary>
	/// Creates an error response for the given request object
	/// </summary>
	/// <param name="request">A JSON-RPC request</param>
	/// <param name="error">An error description</param>
	/// <returns>A new instance of the <see cref="JrpcResponse"/> with the id of the given request</returns>
	public static JrpcResponse CreateError(this JrpcRequest request, JrpcError error)
		=> new(error, request.ID ?? throw JrpcException.CannotCreateResponseForNotification());
}
