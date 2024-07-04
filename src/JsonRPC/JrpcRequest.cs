using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Represents a single JSON-RPC request.
/// </summary>
public readonly record struct JrpcRequest : IJrpcObject
{
	/// <summary>
	/// Initializes a new instance of the <see cref="JrpcRequest"/> structure.
	/// </summary>
	/// <param name="method">A JSON-RPC method name.</param>
	/// <param name="params">An array with parameter values</param>
	/// <param name="id">A JSON-RPC request identifier.</param>
	internal JrpcRequest(string method, JsonNode? @params, IJrpcID? id)
	{
		Method = method;
		Params = @params;
		ID = id;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="JrpcRequest"/> structure without parameters.
	/// </summary>
	/// <param name="method">A JSON-RPC method name.</param>
	/// <param name="id">A JSON-RPC request identifier.</param>
	public JrpcRequest(string method, IJrpcID? id)
		: this(method, (JsonNode?)null, id) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="JrpcRequest"/> structure with an JSON array as parameters.
	/// </summary>
	/// <param name="method">A JSON-RPC method name.</param>
	/// <param name="params">An array with parameter values</param>
	/// <param name="id">A JSON-RPC request identifier.</param>
	public JrpcRequest(string method, JsonArray? @params, IJrpcID? id)
		: this(method, (JsonNode?)@params, id) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="JrpcRequest"/> structure with an JSON object as parameters.
	/// </summary>
	/// <param name="method">A JSON-RPC method name.</param>
	/// <param name="params">An object with parameter values</param>
	/// <param name="id">A JSON-RPC request identifier.</param>
	public JrpcRequest(string method, JsonObject? @params, IJrpcID? id)
		: this(method, (JsonNode?)@params, id) { }

	/// <summary>
	/// The request identifier
	/// </summary>
	public IJrpcID? ID { get; }

	/// <summary>
	/// The request method name
	/// </summary>
	public string Method { get; }

	/// <summary>
	/// The request parameters
	/// </summary>
	public JsonNode? Params { get; }

	/// <inheritdoc/>>
	public JrpcObjectType Type => ID is null ? JrpcObjectType.Notification : JrpcObjectType.Request; 
}
