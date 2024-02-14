using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="JsonRpcRequest"/> class
/// </summary>
public static class JsonRpcRequestExtensions
{
	/// <summary>
	/// Creates a successful response for the given request object
	/// </summary>
	/// <param name="request">A JSON-RPC request</param>
	/// <param name="result">A result</param>
	/// <returns>A new instance of the <see cref="JsonRpcResponse"/> with the id of the given request</returns>
	public static JsonRpcResponse CreateResult(this JsonRpcRequest request, JsonNode result)
		=> new(result, request.ID);

	/// <summary>
	/// Creates an error response for the given request object
	/// </summary>
	/// <param name="request">A JSON-RPC request</param>
	/// <param name="result">An error description</param>
	/// <returns>A new instance of the <see cref="JsonRpcResponse"/> with the id of the given request</returns>
	public static JsonRpcResponse CreateError(this JsonRpcRequest request, JsonRpcError error)
		=> new(error, request.ID);
}
