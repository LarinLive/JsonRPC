using System.IO;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LarinLive.JsonRPC;

/// <summary>
/// The serializer for JSON-RPC objects.
/// </summary>
public sealed class JrpcSerializer
{
	private readonly JrpcExceptionSerializerOptions _exceptionSerializerOptions;

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcSerializer"/> class.
	/// </summary>
	/// <param name="exceptionSerializationOptions">An optional exception serializaion parameters.</param>
	public JrpcSerializer(JrpcExceptionSerializerOptions? exceptionSerializationOptions = null)
	{
		_exceptionSerializerOptions = exceptionSerializationOptions ?? JrpcExceptionSerializerOptions.Default;
	}

	public void WriteTo(JrpcRequest request, Utf8JsonWriter writer)
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

	public void WriteTo(JrpcPacket<JrpcRequest> request, Utf8JsonWriter writer)
	{
		if (request.IsBatch)
		{
			writer.WriteStartArray();
			for (var i = 0; i < request.Batch.Length; i++)
				WriteTo(request.Batch[i], writer);
			writer.WriteEndArray();
		}
		else
			WriteTo(request.Item, writer);
	}

	private void WriteException(Exception e, Utf8JsonWriter writer)
	{
		writer.WriteStartObject("exception");
		writer.WriteString("type", e.GetType().Name);
		writer.WriteString("source", e.Source);
		writer.WriteString("message", e.Message);
		if (_exceptionSerializerOptions.IncludeStackTrace)
			writer.WriteString("stackTrace", e.StackTrace);
		if (e.Data is not null)
		{
			writer.WriteStartObject("data");
			var data = new JsonObject();
			var ms = new MemoryStream();
			foreach (var key in e.Data.Keys.OfType<string>())
			{
				writer.WritePropertyName(key);
				var value = e.Data[key];
				JsonSerializer.Serialize(writer, value, _exceptionSerializerOptions.JsonSerializerOptions ?? JsonSerializerOptions.Default);
				data.AddProperty(key, JsonNode.Parse(ms));
			}
			writer.WriteEndObject();
		}

		if (_exceptionSerializerOptions.IncludeInnerExceptions && e.InnerException is not null)
			WriteException(e.InnerException, writer);
		writer.WriteEndObject();
	}


	public void WriteTo(JrpcResponse response, Utf8JsonWriter writer)
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
			var error = response.Error;
			writer.WriteStartObject("error");
			writer.WriteNumber("code", error.Code);
			writer.WriteString("message", error.Message);
			if (error.Data is not null)
			{
				writer.WritePropertyName("data");
				error.Data.WriteTo(writer);
			}
			else if (error.Exception is not null)
			{
				writer.WriteStartObject("data");
				WriteException(error.Exception, writer);
				writer.WriteEndObject();
			}

			writer.WriteEndObject();
 		}
		writer.WriteEndObject();
	}

	public void WriteTo(JrpcPacket<JrpcResponse> response, Utf8JsonWriter writer)
	{
		if (response.IsBatch)
		{
			writer.WriteStartArray();
			for (var i = 0; i < response.Batch.Length; i++)
				WriteTo(response.Batch[i], writer);
			writer.WriteEndArray();
		}
		else
			WriteTo(response.Item, writer);
	}

	/*
	public JsonObject ToJsonNode(JrpcRequest request)
	{
		var result = new JsonObject()
			.AddProperty("jsonrpc", "2.0")
			.AddProperty("id", request.ID?.ToJsonValue())
			.AddProperty("method", request.Method);
		if (request.Params is not null)
			result.AddProperty("params", request.Params);
		return result;
	}

	public JsonNode ToJsonNode(JrpcPacket<JrpcRequest> request)
	{
		JsonNode result;
		if (request.IsBatch)
			result = new JsonArray(request.Batch!.Select(ToJsonNode).ToArray());
		else
			result = ToJsonNode(request.Item!);
		return result;
	}

	public JsonObject ToJsonNode(JrpcResponse response)
	{
		var result = new JsonObject()
			.AddProperty("jsonrpc", "2.0")
			.AddProperty("id", response.ID?.ToJsonValue());
		if (response.IsSuccess)
			result.AddProperty("result", response.Result);
		else
		{
			var error = response.Error;
			var e = new JsonObject()
				.AddProperty("code", error.Code)
				.AddProperty("message", error.Message);
			if (error.Data is not null)
				e.AddProperty("data", error.Data);
			else if (error.Exception is not null)
				e.AddProperty("data", error.Data);
			result.AddProperty("error", e);
		}
		return result;
	}

	public JsonNode ToJsonNode(JrpcPacket<JrpcResponse> response)
	{
		JsonNode result;
		if (response.IsBatch)
			result = new JsonArray(response.Batch!.Select(ToJsonNode).ToArray());
		else
			result = ToJsonNode(response.Item!);
		return result;
	}*/
}
