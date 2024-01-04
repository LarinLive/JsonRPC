using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Larine.JsonRPC;

/// <summary>
/// Extensions methods for the <see cref="JsonNode"/> class
/// </summary>
public static class JsonNodeExtensions
{
	private static JsonRpcID? GetID(JsonValue? id)
	{
		var value = id?.GetValue<object>();
		if (value is JsonElement element)
		{
			return element.ValueKind switch
			{
				JsonValueKind.Number => new JsonRpcID<long>((long)id!),
				JsonValueKind.String => new JsonRpcID<string>((string)id!),
				_ => throw JsonRpcException.ThrowUnsupportedIdentifierType()
			};
		}
		else
			return null;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JsonRpcRequest ParseRequestItem(JsonNode input)
	{
		var method = (string)input["method"]!;
		var idPropertyValue = input["id"]?.AsValue();
		var id = GetID(idPropertyValue);
		return new JsonRpcRequest(method, input["params"], id);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JsonRpcPacket<JsonRpcRequest> ParseJsonRpcRequest(this JsonNode input)
	{
		if (input is JsonArray array)
		{
			var items = array.ToArray();
			var requests = new JsonRpcRequest[items.Length];
			for (var i = 0; i < array.ToArray().Length; i++)
				requests[i] = ParseRequestItem(items[i]!);
			return requests;
		}
		else
			return ParseRequestItem(input);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JsonRpcResponse ParseResponseItem(JsonNode input)
	{
		var resultProperty = input["result"]?.AsValue();
		var errorProperty = input["error"]?.AsValue();
		var idPropertyValue = input["id"]?.AsValue();
		var id = GetID(idPropertyValue);
		if (resultProperty is not null)
			return new JsonRpcResponse(resultProperty, id);
		else
		{
			var error = errorProperty!.AsObject();
			var code = (int)error["code"]!;
			var message = (string)error["message"]!;
			var dataProperty = error["data"]?.AsValue();
			return new JsonRpcResponse(new JsonRpcError(code, message, dataProperty), id);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JsonRpcPacket<JsonRpcResponse> ParseJsonRpcResponse(this JsonNode input)
	{
		if (input is JsonArray array)
		{
			var items = array.ToArray();
			var responses = new JsonRpcResponse[items.Length];
			for (var i = 0; i < items.Length; i++)
				responses[i] = ParseResponseItem(items[i]!);
			return responses;
		}
		else
			return ParseResponseItem(input);
	}

	public static (JsonRpcPacket<JsonRpcRequest> Request, JsonRpcPacket<JsonRpcResponse> Response) ToJsonRpcObject(this JsonNode input)
	{
		var result1 = JsonRpcSchema.Request.Evaluate(input);
		if (result1.IsValid)
		{
			var requests = input.ParseJsonRpcRequest();
			return (requests, JsonRpcPacket<JsonRpcResponse>.Empty);
		}
		else
		{
			var result2 = JsonRpcSchema.Response.Evaluate(input);
			if (result2.IsValid)
			{
				var responses = input.ParseJsonRpcResponse();
				return (JsonRpcPacket<JsonRpcRequest>.Empty, responses);
			}
			else
				throw new JsonRpcException("Input does not contain a valid JSON-RPC request") {  SchemaEvaluationResult = new[] { result1, result2 } };
		}
	}

	public static JsonRpcPacket<JsonRpcRequest> ToJsonRpcRequest(this JsonNode input)
	{
		var result = JsonRpcSchema.Request.Evaluate(input);
		if (result.IsValid)
		{
			var requests = input.ParseJsonRpcRequest();
			return requests;
		}
		else
			throw new JsonRpcException("Input does not contain a valid JSON-RPC request") { SchemaEvaluationResult = new[] { result } };
	}

	public static JsonRpcPacket<JsonRpcResponse> ToJsonRpcResponse(this JsonNode input)
	{
		var result = JsonRpcSchema.Response.Evaluate(input);
		if (result.IsValid)
		{
			var responses = input.ParseJsonRpcResponse();
			return responses;
		}
		else
			throw new JsonRpcException("Input does not contain a valid JSON-RPC response") { SchemaEvaluationResult = new[] { result } };
	}
}
