using Json.Schema;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Larine.JsonRPC.Dispatcher;

/// <summary>
/// The base class for the <see cref="IJsonRpcDispatcher"/> implementations
/// </summary>
public abstract class DispatcherBase : IJsonRpcDispatcher
{
	/// <summary>
	/// Executes a JSON-RPC request asynchronously.
	/// </summary>
	/// <param name="request">A JSON-RPC request (or a batch of requests) to be performed</param>
	/// <param name="ct">An optional cancellation token.</param>
	/// <returns>A task with a JSON-RPC result.</returns>
	public async Task<JsonRpcPacket<JsonRpcResponse>> ExecuteAsync(JsonRpcPacket<JsonRpcRequest> request, CancellationToken ct = default)
	{
		var result = JsonRpcPacket<JsonRpcResponse>.Empty;
		if (request.IsBatch)
		{
			var batch = new ConcurrentDictionary<JsonRpcRequest, JsonRpcResponse?>();
			await Parallel.ForEachAsync(request.ToArray(), ct,
				async (r, c) =>
				{
					var response = await ExecuteRequestItemAsync(r, c);
					batch.TryAdd(r, response);
				});
			var batchResult = batch.Values.Where(v => v is not null).ToArray();
			if (batchResult.Length > 0)
				result = batchResult!;
		}
		else
		{
			var singleRequest = request.Item!;
			JsonRpcResponse? itemResult;
			try
			{
				itemResult = await ExecuteRequestItemAsync(singleRequest, ct);
			}
			catch (Exception e)
			{
				itemResult = HandleException(singleRequest, e);
			}
			if (itemResult is not null)
				result = itemResult;
		}
		return result;
	}

	/// <inheritdoc/>
	protected abstract ValueTask<JsonRpcResponse?> ExecuteRequestItemAsync(JsonRpcRequest request, CancellationToken ct);

	/// <summary>
	/// Creates a JSON-RPC error for the given request exception
	/// </summary>
	/// <param name="request">A request whose execution was broken</param>
	/// <param name="exception">An error occured while the request execution</param>
	/// <returns></returns>
	protected virtual JsonRpcResponse? HandleException(JsonRpcRequest request, Exception exception)
		=> request.CreateError(JsonRpcError.InternalError);
}
