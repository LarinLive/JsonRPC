using System.Threading;
using System.Threading.Tasks;

namespace Larine.JsonRPC.Dispatcher;

/// <summary>
/// The JSON-RPC dispatcher base interface
/// </summary>
public interface IJsonRpcDispatcher
{
	/// <summary>
	/// Executes a JSON-RPC request asynchronously.
	/// </summary>
	/// <param name="request">The JSON-RPC request to be performed.</param>
	/// <param name="ct">The optional cancellation token.</param>
	/// <returns>A task with a JSON-RPC result.</returns>
	Task<JsonRpcPacket<JsonRpcResponse>> ExecuteAsync(JsonRpcPacket<JsonRpcRequest> request, CancellationToken ct = default);
}
