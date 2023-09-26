using Larine.JsonRPC.Dispatcher;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Larine.JsonRPC.OpenRPC.Dispatcher;

public class JsonRpcDelegatedDispatcher : IJsonRpcDispatcher
{
	private readonly IReadOnlyDictionary<string, (OpenRpcMethodDescriptor, Func<JsonRpcRequest, CancellationToken, Task<JsonRpcResponse?>>)> _methods;

	public JsonRpcDelegatedDispatcher(IReadOnlyDictionary<OpenRpcMethodDescriptor, Func<JsonRpcRequest, CancellationToken, Task<JsonRpcResponse?>>> methods)
	{
		_methods = methods.ToDictionary(k => k.Key.Name, v => (v.Key, v.Value));
	}

	private async ValueTask<JsonRpcResponse?> ProcessSingleItemAsync(JsonRpcRequest request, CancellationToken ct)
	{
		var isNotNotification = request.ID is not null;
		if (_methods.TryGetValue(request.Method, out var descriptor))
		{
			var (definition, method) = descriptor;
			if (definition.Params is not null)
			{
				var paramValidationResult = definition.Params.Evaluate(request.Params);
				if (!paramValidationResult.IsValid)
					return isNotNotification ? request.CreateError(JsonRpcError.InvalidParams) : null;
			}
			var response2 = await method(request, ct);
			return isNotNotification ? response2 : null;
		}
		else 
			return isNotNotification ? request.CreateError(JsonRpcError.MethodNotFound) : null;
	}


	public async Task<JsonRpcPacket<JsonRpcResponse>> DispatchAsync(JsonRpcPacket<JsonRpcRequest> request, CancellationToken ct = default)
	{
		var result = JsonRpcPacket<JsonRpcResponse>.Empty;
		if (request.IsBatch)
		{
			var batch = new ConcurrentDictionary<JsonRpcRequest, JsonRpcResponse?>();
			await Parallel.ForEachAsync(request.ToArray(), ct,
				async (r, c) =>
				{
					var response = await ProcessSingleItemAsync(r, c);
					batch.TryAdd(r, response);
				});
			var batchResult = batch.Values.Where(v => v is not null).ToArray();
			if (batchResult.Length > 0)
				result = batchResult!;
		}
		else
		{
			var itemResult = await ProcessSingleItemAsync(request.Item!, ct);
			if (itemResult is not null)
				result = itemResult;
		}
		return result;
	}
}
 