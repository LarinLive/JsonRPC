namespace LarinLive.JsonRPC;

/// <summary>
/// The base interface for JSON-RPC objects
/// </summary>
public interface IJrpcObject
{
	/// <summary>
	/// Type of the JSON-RPC object
	/// </summary>
	JrpcObjectType Type { get; }
}
