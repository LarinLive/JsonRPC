using System;

namespace Larin.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="Guid"/> values.
/// </summary>
public static class JsonRpcGuidExtensions
{
	/// <summary>
	/// Converts a specified <see cref="Guid"/> to a JSON-RPC identifier
	/// </summary>
	/// <param name="guid">A value</param>
	/// <returns>A new instance of the <see cref="JsonRpcID{String}"/> type.</returns>
	public static JsonRpcID<string> ToJsonRpcID(this Guid guid) => new(guid.ToString("D"));
}
