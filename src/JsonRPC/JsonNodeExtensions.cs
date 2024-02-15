using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Larin.JsonRPC;

/// <summary>
/// Extensions methods for the <see cref="JsonNode"/> class
/// </summary>
public static class JsonNodeExtensions
{
	private static IJrpcID? GetID(JsonValue? id)
	{
		var value = id?.GetValue<object>();
		if (value is JsonElement element)
		{
			return element.ValueKind switch
			{
				JsonValueKind.Number => new JrpcID<long>((long)id!),
				JsonValueKind.String => new JrpcID<string>((string)id!),
				_ => throw JrpcException.CreateUnsupportedIdentifierType()
			};
		}
		else
			return null;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JrpcRequest ParseRequestItem(JsonNode input)
	{
		var method = (string)input["method"]!;
		var idPropertyValue = input["id"]?.AsValue();
		var id = GetID(idPropertyValue);
		return new JrpcRequest(method, input["params"], id);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JrpcPacket<JrpcRequest> ParseJsonRpcRequest(this JsonNode input)
	{
		if (input is JsonArray array)
		{
			var items = array.ToArray();
			var requests = new JrpcRequest[items.Length];
			for (var i = 0; i < array.ToArray().Length; i++)
				requests[i] = ParseRequestItem(items[i]!);
			return requests;
		}
		else
			return ParseRequestItem(input);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JrpcResponse ParseResponseItem(JsonNode input)
	{
		var resultProperty = input["result"]?.AsValue();
		var errorProperty = input["error"]?.AsValue();
		var idPropertyValue = input["id"]?.AsValue();
		var id = GetID(idPropertyValue);
		if (resultProperty is not null)
			return new JrpcResponse(resultProperty, id);
		else
		{
			var error = errorProperty!.AsObject();
			var code = (int)error["code"]!;
			var message = (string)error["message"]!;
			var dataProperty = error["data"]?.AsValue();
			return new JrpcResponse(new JrpcError(code, message, dataProperty), id);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JrpcPacket<JrpcResponse> ParseJsonRpcResponse(this JsonNode input)
	{
		if (input is JsonArray array)
		{
			var items = array.ToArray();
			var responses = new JrpcResponse[items.Length];
			for (var i = 0; i < items.Length; i++)
				responses[i] = ParseResponseItem(items[i]!);
			return responses;
		}
		else
			return ParseResponseItem(input);
	}

	public static (JrpcPacket<JrpcRequest> Request, JrpcPacket<JrpcResponse> Response) ToJsonRpcObject(this JsonNode input)
	{
		var result1 = JrpcSchema.Request.Evaluate(input);
		if (result1.IsValid)
		{
			var requests = input.ParseJsonRpcRequest();
			return (requests, JrpcPacket<JrpcResponse>.Empty);
		}
		else
		{
			var result2 = JrpcSchema.Response.Evaluate(input);
			if (result2.IsValid)
			{
				var responses = input.ParseJsonRpcResponse();
				return (JrpcPacket<JrpcRequest>.Empty, responses);
			}
			else
				throw new JrpcException("Input does not contain a valid JSON-RPC request") {  SchemaEvaluationResult = new[] { result1, result2 } };
		}
	}

	public static JrpcPacket<JrpcRequest> ToJsonRpcRequest(this JsonNode input)
	{
		var result = JrpcSchema.Request.Evaluate(input);
		if (result.IsValid)
		{
			var requests = input.ParseJsonRpcRequest();
			return requests;
		}
		else
			throw new JrpcException("Input does not contain a valid JSON-RPC request") { SchemaEvaluationResult = new[] { result } };
	}

	public static JrpcPacket<JrpcResponse> ToJsonRpcResponse(this JsonNode input)
	{
		var result = JrpcSchema.Response.Evaluate(input);
		if (result.IsValid)
		{
			var responses = input.ParseJsonRpcResponse();
			return responses;
		}
		else
			throw new JrpcException("Input does not contain a valid JSON-RPC response") { SchemaEvaluationResult = new[] { result } };
	}
}
