using System;
using System.Threading;
using System.Threading.Tasks;

namespace Larin.JsonRPC.Dispatcher;

/// <summary>
/// A JSON-RPC method with the handler as a delegate
/// </summary>
public class JsonRpcDelegatedMethod : JsonRpcMethodBase
{
	private readonly Func<JrpcRequest, CancellationToken, Task<JrpcResponse?>> _func;

	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcDelegatedMethod"/> class
	/// </summary>
	/// <param name="name">A JSON-RPC method name</param>
	/// <param name="func">A delegate</param>
	public JsonRpcDelegatedMethod(string name, Func<JrpcRequest, CancellationToken, Task<JrpcResponse?>> func) : base(name)
	{
		_func = func;
	}

	/// <inheritdoc/>
	public override Task<JrpcResponse?> ExecuteAsync(JrpcRequest request, CancellationToken ct)
		=> _func(request, ct);
}
