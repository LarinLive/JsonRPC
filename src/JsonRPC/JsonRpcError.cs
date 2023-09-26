using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

public sealed class JsonRpcError
{
	public static readonly JsonRpcError ParseError = new(-32700, "Parse error", null);
	public static readonly JsonRpcError InvalidRequest = new (-32600, "Invalid Request", null);
	public static readonly JsonRpcError MethodNotFound = new(-32601, "Method not found", null);
	public static readonly JsonRpcError InvalidParams = new (-32602, "Invalid params", null);
	public static readonly JsonRpcError InternalError = new (-32603, "Internal error", null);

	public JsonRpcError(int code, string message, JsonNode? data)
	{
		Code = code;
		Message = message;
		Data = data;
	}

	public int Code { get; }

	public string Message { get; }

	public JsonNode? Data { get; }

	public JsonRpcError WithData(JsonNode? data)
	{
		return new JsonRpcError(Code, Message, data);
	}
};
