using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

public class JsonRpcRequest : JsonRpcObject
{
	internal JsonRpcRequest(string method, JsonNode? @params, IJsonRpcID? id)
	{
		Method = method;
		Params = @params;
		ID = id;
	}

	public JsonRpcRequest(string method, IJsonRpcID? id)
		: this(method, (JsonNode?)null, id) { }

	public JsonRpcRequest(string method, JsonArray? @params, IJsonRpcID? id)
		: this(method, (JsonNode?)@params, id) { }

	public JsonRpcRequest(string method, JsonObject? @params, IJsonRpcID? id)
		: this(method, (JsonNode?)@params, id) { }


	public string Method { get; }
	
	public JsonNode? Params { get; }

	public IJsonRpcID? ID { get; }

	public override JsonRpcObjectType Type => JsonRpcObjectType.Request;
}
