using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LarinLive.JsonRPC.Dispatcher;

/// <summary>
/// The base class for the <see cref="IJrpcDispatcher"/> implementations
/// </summary>
public abstract class JrpcDispatcherBase : IJrpcDispatcher
{
	/// <summary>
	/// Executes a JSON-RPC request asynchronously.
	/// </summary>
	/// <param name="request">A JSON-RPC request (or a batch of requests) to be performed</param>
	/// <param name="ct">An optional cancellation token.</param>
	/// <returns>A task with a JSON-RPC result.</returns>
	public async Task<JrpcPacket<JrpcResponse>> ExecuteAsync(JrpcPacket<JrpcRequest> request, CancellationToken ct = default)
	{
		var result = JrpcPacket<JrpcResponse>.Empty;
		if (request.IsBatch)
		{
			var batch = new ConcurrentDictionary<JrpcRequest, JrpcResponse?>();
			await Parallel.ForEachAsync(request.ToArray(), ct,
				async (r, c) =>
				{
					var itemResult = await DoExecuteRequestItemAsync(r, c);
					if (itemResult.HasValue)
						batch.TryAdd(r, itemResult.Value);
				});
			var batchResult = batch.Values.Where(v => v is not null).Select(v => v!.Value).ToArray();
			if (batchResult.Length > 0)
				result = batchResult!;
			else
				result = new JrpcPacket<JrpcResponse>();
		}
		else
		{
			var singleRequest = request.Item!;
			var itemResult = await DoExecuteRequestItemAsync(singleRequest, ct);
			if (itemResult.HasValue)
				result = itemResult.Value;
		}
		return result;
	}

	private async Task<JrpcResponse?> DoExecuteRequestItemAsync(JrpcRequest request, CancellationToken ct)
	{
		try
		{
			return await ExecuteRequestItemAsync(request, ct);
		}
		catch (Exception e)
		{
			return request.ID is not null ? HandleException(request, e) : null;
		}
	}

	/// <summary>
	/// Executes a single JSON-RPC request item asynchronously.
	/// </summary>
	/// <param name="request">A JSON-RPC request item to be executed.</param>
	/// <param name="ct">An optional cancellation token.</param>
	/// <returns>A task with a JSON-RPC result.</returns>
	protected abstract Task<JrpcResponse?> ExecuteRequestItemAsync(JrpcRequest request, CancellationToken ct);

	/// <summary>
	/// Creates a JSON-RPC error for the given request exception.
	/// </summary>
	/// <param name="request">A request whose execution was broken.</param>
	/// <param name="exception">An error occured while the request execution.</param>
	/// <returns></returns>
	protected virtual JrpcResponse? HandleException(JrpcRequest request, Exception exception)
		=> request.CreateError(JrpcError.InternalError.WithException(exception));
}
