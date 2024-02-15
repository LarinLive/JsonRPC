using System.Collections.Generic;
using System.Linq;

namespace Larin.JsonRPC;

/// <summary>
/// 
/// </summary>
public static class JrpcPacketExtensions
{
	/// <summary>
	/// Converts a JSON-RPC request to a JSON-RPC request packet.
	/// </summary>
	/// <param name="request">An instance of the <see cref="JrpcRequest"/> class to be converted.</param>
	/// <returns>A new instance of the <see cref="JrpcPacket{JsonRpcRequest}"/> class.</returns>
	public static JrpcPacket<JrpcRequest> AsPacket(this JrpcRequest request) => request;

	/// <summary>
	/// Converts a batch of JSON-RPC requests to a JSON-RPC request packet.
	/// </summary>
	/// <param name="requests">An array of the <see cref="JrpcRequest"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JrpcPacket{JsonRpcRequest}"/> class.</returns>
	public static JrpcPacket<JrpcRequest> AsPacket(this JrpcRequest[] requests) => requests;

	/// <summary>
	/// Converts a batch of JSON-RPC requests to a JSON-RPC request packet.
	/// </summary>
	/// <param name="requests">A sequence of the <see cref="JrpcRequest"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JrpcPacket{JsonRpcRequest}"/> class.</returns>
	public static JrpcPacket<JrpcRequest> AsPacket(this IEnumerable<JrpcRequest> requests) => requests.ToArray();

	/// <summary>
	/// Converts a JSON-RPC response to a JSON-RPC response packet.
	/// </summary>
	/// <param name="response">An instance of the <see cref="JrpcResponse"/> class to be converted.</param>
	/// <returns>A new instance of the <see cref="JrpcPacket{JsonRpcResponse}"/> class.</returns>
	public static JrpcPacket<JrpcResponse> AsPacket(this JrpcResponse response) => response;

	/// <summary>
	/// Converts a batch of JSON-RPC responses to a JSON-RPC response packet.
	/// </summary>
	/// <param name="responses">An array of the <see cref="JrpcResponse"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JrpcPacket{JsonRpcResponse}"/> class.</returns>
	public static JrpcPacket<JrpcResponse> AsPacket(this JrpcResponse[] responses) => responses;

	/// <summary>
	/// Converts a batch of JSON-RPC responses to a JSON-RPC response packet.
	/// </summary>
	/// <param name="responses">A sequence of the <see cref="JrpcResponse"/> class instances to be converted.</param>
	/// <returns>A new instance of the <see cref="JrpcPacket{JsonRpcResponse}"/> class.</returns>
	public static JrpcPacket<JrpcResponse> AsPacket(this IEnumerable<JrpcResponse> responses) => responses.ToArray();
}
