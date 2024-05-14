using Json.Schema;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LarinLive.JsonRPC.Dispatcher;

/// <summary>
/// A JSON-RPC method with the handler as a delegate
/// </summary>
public class JrpcDelegatedMethod : JrpcMethodBase
{
	private readonly Func<JrpcRequest, CancellationToken, Task<JrpcResponse?>> _func;

	protected JrpcDelegatedMethod(string name, Func<JrpcRequest, CancellationToken, Task<JrpcResponse?>> func)
		: base(name)
	{
		_func = func;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcDelegatedMethod"/> class.
	/// </summary>
	/// <param name="name">A JSON-RPC method name.</param>
	/// <param name="func">A JSON-RPC method delegate.</param>
	/// <param name="paramsSchema">A JSON schema for a JSON-RPC request params.</param>
	public static JrpcDelegatedMethod Create(string name, Func<JrpcRequest, CancellationToken, Task<JrpcResponse?>> func, JsonSchema? paramsSchema = null)
	{
		var method = new JrpcDelegatedMethod(name, func) { ParamsSchema = paramsSchema };
		return method;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcDelegatedMethod"/> class using <see cref="JrpcMethodAttribule"/> as a source for the method information.
	/// </summary>
	/// <param name="func">A JSON-RPC method delegate.</param>
	public static JrpcDelegatedMethod Create(Func<JrpcRequest, CancellationToken, Task<JrpcResponse?>> func)
	{
		var methodInfo = func.GetMethodInfo();
		var attr = methodInfo?.GetCustomAttributes(typeof(JrpcMethodAttribule)).OfType<JrpcMethodAttribule>().FirstOrDefault();
		if (attr is null)
			throw new ArgumentException("The provided method should be marked with the [JrpcMethod] attribute.");
		var methodName = attr.MethodName ?? $"{methodInfo!.DeclaringType!.Name}.{methodInfo.Name}";
		JsonSchema? paramsSchema = null;
		if (attr.ParamsSchemaResourceName is not null) 
		{
			var asm = methodInfo!.DeclaringType!.Assembly;
			var stream = asm.GetManifestResourceStream(attr.ParamsSchemaResourceName)!;
			paramsSchema = JsonSchema.FromStream(stream).AsTask().Result;
		}
		var method = new JrpcDelegatedMethod(methodName, func) { ParamsSchema = paramsSchema };
		return method;
	}

	/// <inheritdoc/>
	public override Task<JrpcResponse?> ExecuteAsync(JrpcRequest request, CancellationToken ct)
		=> _func(request, ct);
}
