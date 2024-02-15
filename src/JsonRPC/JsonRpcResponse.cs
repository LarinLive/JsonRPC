using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

public sealed class JsonRpcResponse : JsonRpcObject
{
	public JsonRpcResponse(JsonNode? result, IJsonRpcID? id)
	{
		IsSuccess = true;
		Result = result;
		ID = id;
	}

	public JsonRpcResponse(JsonRpcError error, IJsonRpcID? id)
	{
		IsSuccess = false;
		Error = error;
		ID = id;
	}

	public bool IsSuccess { get; }

	public JsonNode? Result { get; }

	public JsonRpcError? Error { get; }

	public IJsonRpcID? ID { get; }

	public override JsonRpcObjectType Type => JsonRpcObjectType.Response;
}
	
