using Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Larin.JsonRPC.Dispatcher;

/// <summary>
/// The main implementation JSON-RPC dispatcher
/// </summary>
public class JsonRpcDispatcher : JsonRpcDispatcherBase
{
	private readonly Dictionary<string, JsonRpcMethodBase> _methods;

	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcDispatcher"/> class
	/// </summary>
	/// <param name="methods">A JSON-RPC method handlers dictionary</param>
	public JsonRpcDispatcher(IReadOnlyCollection<JsonRpcMethodBase> methods)
	{
		_methods = methods.ToDictionary(m => m.Name, m => m);
	}

	/// <inheritdoc/>
	protected override async ValueTask<JsonRpcResponse?> ExecuteRequestItemAsync(JsonRpcRequest request, CancellationToken ct)
	{
		var isNotNotification = request.ID is not null;
		if (_methods.TryGetValue(request.Method, out var descriptor))
		{
			if (descriptor.Params is not null)
			{
				var paramValidationResult = descriptor.Params.Evaluate(request.Params);
				if (!paramValidationResult.IsValid)
					return isNotNotification ? request.CreateError(JsonRpcError.InvalidParams) : null;
			}
			var response2 = await descriptor.ExecuteAsync(request, ct);
			return isNotNotification ? response2 : null;
		}
		else
			return isNotNotification ? request.CreateError(JsonRpcError.MethodNotFound) : null;
	}
}
