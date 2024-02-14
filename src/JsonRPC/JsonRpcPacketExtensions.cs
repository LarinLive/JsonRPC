using System.Linq;
using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

public static class JsonRpcPacketExtensions
{
	private static JsonObject ToJsonObject(this JsonRpcRequest request)
	{
		var result = new JsonObject()
			.AddProperty("jsonrpc", "2.0")
			.AddProperty("id", request.ID?.ToJsonValue())
			.AddProperty("method", request.Method);
		if (request.Params is not null)
			result.AddProperty("params", request.Params);
		return result;
	}

	public static JsonNode ToJsonNode(this JsonRpcPacket<JsonRpcRequest> request)
	{
		JsonNode result;
		if (request.IsBatch)
			result = new JsonArray(request.Batch!.Select(i => i.ToJsonObject()).ToArray());
		else
			result = request.Item!.ToJsonObject();
		return result;
	}

	private static JsonObject ToJsonObject(this JsonRpcResponse response)
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
		}
		return result;
	}

	public static JsonNode ToJsonNode(this JsonRpcPacket<JsonRpcResponse> response)
	{
		JsonNode result;
		if (response.IsBatch)
			result = new JsonArray(response.Batch!.Select(i => i.ToJsonObject()).ToArray());
		else
			result = response.Item!.ToJsonObject();
		return result;
	}
}
