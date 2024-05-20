using Json.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LarinLive.JsonRPC.Dispatcher;

/// <summary>
/// The delegated JSON-RPC dispatcher
/// </summary>
public class JrpcDelegatedDispatcher : JrpcDispatcherBase
{
	private static readonly EvaluationOptions _schemaEvaluationOptions = new()
	{
		OutputFormat = OutputFormat.Hierarchical,
		RequireFormatValidation = true
	};
	private readonly Dictionary<string, JrpcMethodBase> _methods;

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcDelegatedDispatcher"/> class.
	/// </summary>
	/// <param name="methods">A JSON-RPC method handlers dictionary.</param>
	public JrpcDelegatedDispatcher(IReadOnlyCollection<JrpcMethodBase> methods)
	{
		_methods = methods.ToDictionary(m => m.Name, m => m);
	}

	/// <inheritdoc/>
	protected override async Task<JrpcResponse?> ExecuteRequestItemAsync(JrpcRequest request, CancellationToken ct)
	{
		var isNotNotification = request.ID is not null;
		if (_methods.TryGetValue(request.Method, out var descriptor))
		{
			if (descriptor.ParamsSchema is not null)
			{
				var paramValidationResult = descriptor.ParamsSchema.Evaluate(request.Params, _schemaEvaluationOptions);
				if (!paramValidationResult.IsValid)
				{
					if (isNotNotification)
					{
						var e = new JrpcException($"Params are not valid.");
						e.Data.Add("JrpcRequestParamsEvaluation", paramValidationResult);
						return request.CreateError(JrpcError.InvalidParams.WithException(e));
					}
					else
						return null;
				}
			}
			var response2 = await descriptor.ExecuteAsync(request, ct);
			return isNotNotification ? response2 : null;
		}
		else
			return isNotNotification ? request.CreateError(JrpcError.MethodNotFound) : null;
	}
}
