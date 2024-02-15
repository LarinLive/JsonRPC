using System.Collections.Generic;
using System.Linq;

namespace Larin.JsonRPC;

/// <summary>
/// 
/// </summary>
public static class JsonRpcPacketExtensions
{
	/// <summary>
	/// Converts a JSON-RPC request to a JSON-RPC request packet.
	/// </summary>
	/// <param name="request">An instance of the <see cref="JsonRpcRequest"/> class to be converted.</param>
	/// <returns>A new instance of the <see cref="JsonRpcPacket{JsonRpcRequest}"/> class.</returns>
	public static JsonRpcPacket<JsonRpcRequest> AsPacket(this JsonRpcRequest request) => request;

	/// <summary>
	/// Converts a batch of JSON-RPC requests to a JSON-RPC request packet.
	/// </summary>
	/// <param name="requests">An array of the <see cref="JsonRpcRequest"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JsonRpcPacket{JsonRpcRequest}"/> class.</returns>
	public static JsonRpcPacket<JsonRpcRequest> AsPacket(this JsonRpcRequest[] requests) => requests;

	/// <summary>
	/// Converts a batch of JSON-RPC requests to a JSON-RPC request packet.
	/// </summary>
	/// <param name="requests">A sequence of the <see cref="JsonRpcRequest"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JsonRpcPacket{JsonRpcRequest}"/> class.</returns>
	public static JsonRpcPacket<JsonRpcRequest> AsPacket(this IEnumerable<JsonRpcRequest> requests) => requests.ToArray();

	/// <summary>
	/// Converts a JSON-RPC response to a JSON-RPC response packet.
	/// </summary>
	/// <param name="response">An instance of the <see cref="JsonRpcResponse"/> class to be converted.</param>
	/// <returns>A new instance of the <see cref="JsonRpcPacket{JsonRpcResponse}"/> class.</returns>
	public static JsonRpcPacket<JsonRpcResponse> AsPacket(this JsonRpcResponse response) => response;

	/// <summary>
	/// Converts a batch of JSON-RPC responses to a JSON-RPC response packet.
	/// </summary>
	/// <param name="responses">An array of the <see cref="JsonRpcResponse"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JsonRpcPacket{JsonRpcResponse}"/> class.</returns>
	public static JsonRpcPacket<JsonRpcResponse> AsPacket(this JsonRpcResponse[] responses) => responses;

	/// <summary>
	/// Converts a batch of JSON-RPC responses to a JSON-RPC response packet.
	/// </summary>
	/// <param name="responses">A sequence of the <see cref="JsonRpcResponse"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JsonRpcPacket{JsonRpcResponse}"/> class.</returns>
	public static JsonRpcPacket<JsonRpcResponse> AsPacket(this IEnumerable<JsonRpcResponse> responses) => responses.ToArray();
}
