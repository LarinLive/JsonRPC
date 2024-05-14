using System;

namespace LarinLive.JsonRPC.Dispatcher;

/// <summary>
/// Specifies JSON-RPC method name for a target method
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class JrpcMethodAttribule : Attribute
{
	/// <summary>
	/// Creates a new instance of the <see cref="JrpcMethodAttribule"/> class
	/// </summary>
	/// <param name="methodName"></param>
	/// <param name="paramsSchemaResourceName"></param>
	public JrpcMethodAttribule(string? methodName = null, string? paramsSchemaResourceName = null)
	{
		MethodName = methodName;
		ParamsSchemaResourceName = paramsSchemaResourceName;
	}

	/// <summary>
	/// The JSON-RPC method name
	/// </summary>
	public string? MethodName { get; set; }

	/// <summary>
	/// The name of the managed resource which holds the JSON schema for the params object of a JSON-RPC request
	/// </summary>
	public string? ParamsSchemaResourceName { get; set; }
}
