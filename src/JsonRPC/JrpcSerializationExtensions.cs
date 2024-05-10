using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extesnion methods for JSON-RPC request serialization
/// </summary>
public static class JrpcSerializationExtensions
{
	public static void WriteTo(this JrpcRequest request, Utf8JsonWriter writer)
	{
		writer.WriteStartObject();
		writer.WriteString("jsonrpc", "2.0");
		if (request.ID is not null)
		{
			writer.WritePropertyName("id");
			request.ID.WriteTo(writer);
		}
		writer.WriteString("method", request.Method);
		if (request.Params is not null)
		{
			writer.WritePropertyName("params");
			request.Params.WriteTo(writer);
		}
		writer.WriteEndObject();
	}

	public static void WriteTo(this JrpcPacket<JrpcRequest> request, Utf8JsonWriter writer)
	{
		if (request.IsBatch)
		{
			writer.WriteStartArray();
			for (var i = 0; i < request.Batch.Length; i++)
				request.Batch[i].WriteTo(writer);
			writer.WriteEndArray();
		}
		else
			request.Item.WriteTo(writer);
	}

	public static void WriteTo(this JrpcResponse response, Utf8JsonWriter writer)
	{
		writer.WriteStartObject();
		writer.WriteString("jsonrpc", "2.0");
		writer.WritePropertyName("id");
		response.ID.WriteTo(writer);
		if (response.IsSuccess)
		{
			writer.WritePropertyName("result");
			response.Result!.WriteTo(writer);
		}
		else
		{
			var error = response.Error!;
			writer.WriteStartObject("error");
			writer.WriteNumber("code", error.Code);
			writer.WriteString("message", error.Message);
			if (error.Data is not null)
			{
				writer.WritePropertyName("data");
				error.Data.WriteTo(writer);
			}
			writer.WriteEndObject();
 		}
		writer.WriteEndObject();
	}

	public static void WriteTo(this JrpcPacket<JrpcResponse> response, Utf8JsonWriter writer)
	{
		if (response.IsBatch)
		{
			writer.WriteStartArray();
			for (var i = 0; i < response.Batch.Length; i++)
				response.Batch[i].WriteTo(writer);
			writer.WriteEndArray();
		}
		else
			response.Item.WriteTo(writer);
	}

	public static JsonObject ToJsonNode(this JrpcRequest request)
	{
		var result = new JsonObject()
			.AddProperty("jsonrpc", "2.0")
			.AddProperty("id", request.ID?.ToJsonValue())
			.AddProperty("method", request.Method);
		if (request.Params is not null)
			result.AddProperty("params", request.Params);
		return result;
	}

	public static JsonNode ToJsonNode(this JrpcPacket<JrpcRequest> request)
	{
		JsonNode result;
		if (request.IsBatch)
			result = new JsonArray(request.Batch!.Select(i => i.ToJsonNode()).ToArray());
		else
			result = request.Item!.ToJsonNode();
		return result;
	}

	public static JsonObject ToJsonNode(this JrpcResponse response)
	{
		var result = new JsonObject()
			.AddProperty("jsonrpc", "2.0")
			.AddProperty("id", response.ID?.ToJsonValue());
		if (response.IsSuccess)
			result.AddProperty("result", response.Result);
		else
		{
			var error = response.Error!;
			var e = new JsonObject()
				.AddProperty("code", error.Code)
				.AddProperty("message", error.Message);
			if (error.Data is not null)
				e.AddProperty("data", error.Data);
			result.AddProperty("error", e);
		}
		return result;
	}

	public static JsonNode ToJsonNode(this JrpcPacket<JrpcResponse> response)
	{
		JsonNode result;
		if (response.IsBatch)
			result = new JsonArray(response.Batch!.Select(i => i.ToJsonNode()).ToArray());
		else
			result = response.Item!.ToJsonNode();
		return result;
	}
}
