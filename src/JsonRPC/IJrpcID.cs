using System;
using System.Text.Json.Nodes;

namespace Larin.JsonRPC;

/// <summary>
/// Encapsulates a JSON-RPC request identifier
/// </summary>
public interface IJrpcID : IEquatable<IJrpcID>
{
	/// <summary>
	/// Serializes a JSON-RPC request identifier to a <see cref="JsonValue"/> object .
	/// </summary>
	/// <returns></returns>
	JsonValue ToJsonValue();
}
