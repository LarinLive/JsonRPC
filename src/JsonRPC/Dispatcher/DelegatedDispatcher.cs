using Json.Schema;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Larine.JsonRPC.Dispatcher;

/// <summary>
/// The delegate-based implementation JSON-RPC dispatcher
/// </summary>
public class DelegatedDispatcher : DispatcherBase
{
	private readonly Dictionary<string, (MethodDescriptor, Func<JsonRpcRequest, CancellationToken, ValueTask<JsonRpcResponse?>>)> _methods;

	/// <summary>
	/// Creates a new instance of the <see cref="DelegatedDispatcher"/> class
	/// </summary>
	/// <param name="methods">A JSON-RPC method handlers dictionary</param>
	public DelegatedDispatcher(IReadOnlyDictionary<MethodDescriptor, Func<JsonRpcRequest, CancellationToken, ValueTask<JsonRpcResponse?>>> methods)
	{
		_methods = methods.ToDictionary(k => k.Key.Name, v => (v.Key, v.Value));
	}

	/// <inheritdoc/>
	protected override async ValueTask<JsonRpcResponse?> ExecuteRequestItemAsync(JsonRpcRequest request, CancellationToken ct)
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

	private JsonRpcResponse? HandleInvalidParamsError(JsonRpcRequest request, EvaluationResults evaluationResults)
	{
		return request.CreateError(JsonRpcError.InvalidParams);
	}
}
