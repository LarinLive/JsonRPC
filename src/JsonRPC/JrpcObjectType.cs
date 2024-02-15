namespace Larin.JsonRPC;

/// <summary>
/// Possible JSON-RPC object types
/// </summary>
public enum JrpcObjectType
{
	/// <summary>
	/// A JSON-RPC request
	/// </summary>
	Request,

	/// <summary>
	/// A JSON-RPC notification
	/// </summary>
	Notification,

	/// <summary>
	/// A JSON-RPC responce
	/// </summary>
	Response
}
