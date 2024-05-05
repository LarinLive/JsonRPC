using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LarinLive.JsonRPC.Dispatcher;

/// <summary>
/// The delegated JSON-RPC dispatcher
/// </summary>
public class JsonRpcDelegatedDispatcher : JsonRpcDispatcherBase
{
	private readonly Dictionary<string, JsonRpcMethodBase> _methods;

	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcDelegatedDispatcher"/> class
	/// </summary>
	/// <param name="methods">A JSON-RPC method handlers dictionary</param>
	public JsonRpcDelegatedDispatcher(IReadOnlyCollection<JsonRpcMethodBase> methods)
	{
		_methods = methods.ToDictionary(m => m.Name, m => m);
	}

	/// <inheritdoc/>
	protected override async Task<JrpcResponse?> ExecuteRequestItemAsync(JrpcRequest request, CancellationToken ct)
	{
		var isNotNotification = request.ID is not null;
		if (_methods.TryGetValue(request.Method, out var descriptor))
		{
			if (descriptor.Params is not null)
			{
				var paramValidationResult = descriptor.Params.Evaluate(request.Params);
				if (!paramValidationResult.IsValid)
					return isNotNotification ? request.CreateError(JrpcError.InvalidParams) : null;
			}
			var response2 = await descriptor.ExecuteAsync(request, ct);
			return isNotNotification ? response2 : null;
		}
		else
			return isNotNotification ? request.CreateError(JrpcError.MethodNotFound) : null;
	}
}
