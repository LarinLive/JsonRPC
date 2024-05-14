using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.Json;
using Json.Schema;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extensions methods for the <see cref="JsonNode"/> class
/// </summary>
public static class JrpcObjectDeserializationExtensions
{
	private static readonly EvaluationOptions _schemaEvaluationOptions = new() 
	{ 
		OutputFormat = OutputFormat.Hierarchical,
		RequireFormatValidation = true
	};

	private static IJrpcID GetID(JsonValue? id)
	{
		var value = id?.GetValue<object>();
		if (value is JsonElement element)
		{
			return element.ValueKind switch
			{
				JsonValueKind.Number => new JrpcID<long>((long)id!),
				JsonValueKind.String => new JrpcID<string>((string)id!),
				_ => throw JrpcException.UnsupportedIdentifierType()
			};
		}
		else
			return new JrpcNullID();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JrpcRequest ParseRequestItem(JsonNode input)
	{
		var method = (string)input["method"]!;
		var idProperty = input["id"];
		var idPropertyValue = idProperty?.AsValue();
		var id = idProperty is null ? null : GetID(idPropertyValue);
		return new JrpcRequest(method, input["params"], id);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JrpcPacket<JrpcRequest> ParseJrpcRequest(this JsonNode input)
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
	private static JrpcPacket<JrpcResponse> ParseJrpcResponse(this JsonNode input)
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

	public static (JrpcPacket<JrpcRequest> Request, JrpcPacket<JrpcResponse> Response) ToJrpcObject(this JsonNode input)
	{
		var result1 = JrpcSchema.Request.Evaluate(input, _schemaEvaluationOptions);
		if (result1.IsValid)
		{
			var requests = input.ParseJrpcRequest();
			return (requests, JrpcPacket<JrpcResponse>.Empty);
		}
		else
		{
			var result2 = JrpcSchema.Response.Evaluate(input);
			if (result2.IsValid)
			{
				var responses = input.ParseJrpcResponse();
				return (JrpcPacket<JrpcRequest>.Empty, responses);
			}
			else
			{
				var e = new JrpcException($"An input contains neither a valid JSON-RPC request nor a valid JSON-RPC response.");
				e.Data.Add("JrpcRequestEvaluation", result1);
				e.Data.Add("JrpcResponseEvaluation", result2);
				throw e;
			}
		}
	}

	/// <summary>
	/// Converts a <see cref="JsonNode"/> object to a JSON-RPC request object.
	/// </summary>
	/// <param name="input">An object to convert</param>
	/// <returns>An instance of the <see cref="JrpcPacket{T}"/> struct that incapsulates a JSON-RPC request.</returns>
	/// <exception cref="JrpcException">In case of the given JSON represents an invalid JSON-RPC request.</exception>
	public static JrpcPacket<JrpcRequest> ToJrpcRequest(this JsonNode input)
	{
		var result = JrpcSchema.Request.Evaluate(input, _schemaEvaluationOptions);
		if (result.IsValid)
		{
			var requests = input.ParseJrpcRequest();
			return requests;
		}
		else
		{
			var e = new JrpcException($"An input does not contain a valid JSON-RPC request.");
			e.Data.Add("JrpcRequestEvaluation", result);
			throw e;
		}
	}

	/// <summary>
	/// Converts a <see cref="JsonNode"/> object to a JSON-RPC response object.
	/// </summary>
	/// <param name="input">An object to convert</param>
	/// <returns>An instance of the <see cref="JrpcPacket{T}"/> struct that incapsulates a JSON-RPC response.</returns>
	/// <exception cref="JrpcException">In case of the given JSON represents an invalid JSON-RPC response.</exception>
	public static JrpcPacket<JrpcResponse> ToJrpcResponse(this JsonNode input)
	{
		var result = JrpcSchema.Response.Evaluate(input, _schemaEvaluationOptions);
		if (result.IsValid)
		{
			var responses = input.ParseJrpcResponse();
			return responses;
		}
		else
		{
			var e = new JrpcException($"An input does not contain a valid JSON-RPC response.");
			e.Data.Add("JrpcResponseEvaluation", result);
			throw e;
		}
	}
}
