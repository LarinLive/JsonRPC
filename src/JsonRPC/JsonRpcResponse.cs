using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

public sealed class JsonRpcResponse : JsonRpcObject
{
	public JsonRpcResponse(JsonNode? result, JsonRpcID? id)
	{
		IsSuccess = true;
		Result = result;
		ID = id;
	}

	public JsonRpcResponse(JsonRpcError error, JsonRpcID? id)
	{
		IsSuccess = false;
		Error = error;
		ID = id;
	}

	public bool IsSuccess { get; }

	public JsonNode? Result { get; }

	public JsonRpcError? Error { get; }

	public JsonRpcID? ID { get; }

	public override JsonRpcObjectType Type => JsonRpcObjectType.Response;
}
	
