using Json.Schema;
using System;

namespace Larine.JsonRPC;

/// <summary>
/// A holder for JSON schemas for then JSON-RPC 2.0 objects
/// </summary>
public static class JsonRpcSchema
{
	private static readonly Lazy<JsonSchema> _request = new(BuildRequestSchema);
	private static readonly Lazy<JsonSchema> _response = new(BuildResponseSchema);

	private static JsonSchema BuildRequestSchema()
	{
		var asm = typeof(JsonRpcSchema).Assembly;
		var stream = asm.GetManifestResourceStream("Larine.JsonRPC.schema.request.json")!;
		var schema = JsonSchema.FromStream(stream).AsTask().Result;
		return schema;
	}

	private static JsonSchema BuildResponseSchema()
	{
		var asm = typeof(JsonRpcSchema).Assembly;
		var stream = asm.GetManifestResourceStream("Larine.JsonRPC.schema.response.json")!;
		var schema = JsonSchema.FromStream(stream).AsTask().Result;
		return schema;
	}

	/// <summary>
	/// The JSON schema for a JSON-RPC 2.0 request object
	/// </summary>
	public static JsonSchema Request => _request.Value;

	/// <summary>
	/// The JSON schema for a JSON-RPC 2.0 response object
	/// </summary>
	public static JsonSchema Response => _response.Value;
}
