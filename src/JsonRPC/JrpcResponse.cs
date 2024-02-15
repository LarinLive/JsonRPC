using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

public readonly record struct JrpcResponse : IJrpcObject
{
	public JrpcResponse(JsonNode? result, IJrpcID id)
	{
		IsSuccess = true;
		Result = result;
		ID = id;
	}

	public JrpcResponse(JrpcError error, IJrpcID id)
	{
		IsSuccess = false;
		Error = error;
		ID = id;
	}

	public IJrpcID ID { get; }

	public bool IsSuccess { get; }

	public JsonNode? Result { get; }

	public JrpcError? Error { get; }


	public JrpcObjectType Type { get => JrpcObjectType.Response; }
}

