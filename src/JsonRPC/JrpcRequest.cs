using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

public readonly record struct JrpcRequest : IJrpcObject
{
	internal JrpcRequest(string method, JsonNode? @params, IJrpcID? id)
	{
		Method = method;
		Params = @params;
		ID = id;
	}

	public JrpcRequest(string method, IJrpcID? id)
		: this(method, (JsonNode?)null, id) { }

	public JrpcRequest(string method, JsonArray? @params, IJrpcID? id)
		: this(method, (JsonNode?)@params, id) { }

	public JrpcRequest(string method, JsonObject? @params, IJrpcID? id)
		: this(method, (JsonNode?)@params, id) { }


	public string Method { get; }

	public IJrpcID? ID { get; }

	public JsonNode? Params { get; }

	public JrpcObjectType Type => ID is null ? JrpcObjectType.Notification : JrpcObjectType.Request; 
}
