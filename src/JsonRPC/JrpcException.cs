using System;

namespace LarinLive.JsonRPC;

/// <summary>
/// JSON-RPC parsing exceptions.
/// </summary>
public class JrpcException : ApplicationException
{
	/// <summary>
	/// Creates a new instance of the <see cref="JrpcException"/> class with the specified message.
	/// </summary>
	/// <param name="message">An error message</param>
	public JrpcException(string? message)
		: base(message) { }

	/// <summary>
	/// Creates a new instance of the <see cref="JrpcException"/> class with the specified message and inner exception.
	/// </summary>
	/// <param name="message">An error message</param>
	/// <param name="innerException">An inner exception</param>
	public JrpcException(string? message, Exception? innerException)
		: base(message, innerException) { }

	/// <summary>
	/// Creates an instance of the <see cref="JrpcException"/> class for a unsupported identifier type/
	/// </summary>
	/// <returns>A new instance of the <see cref="JrpcException"/> class.</returns>
	public static JrpcException UnsupportedIdentifierType() => new("Only string and long types are supported for JSON-RPC identifiers.");

	/// <summary>
	/// Creates an instance of the <see cref="JrpcException"/> class for an attempt to create a response for a JSON-RPC notification.
	/// </summary>
	/// <returns>A new instance of the <see cref="JrpcException"/> class.</returns>
	public static JrpcException CannotCreateResponseForNotification() => new("Cannot create a response for a JSON-RPC notification.");

	/// <summary>
	/// Creates an instance of the <see cref="JrpcException"/> class for an empty packet.
	/// </summary>
	/// <returns>A new instance of the <see cref="JrpcException"/> class.</returns>
	public static JrpcException PacketIsEmpty() => new("The JSON-RPC packet is empty.");

	/// <summary>
	/// Creates an instance of the <see cref="JrpcException"/> class for an empty batch.
	/// </summary>
	/// <returns>A new instance of the <see cref="JrpcException"/> class.</returns>
	public static JrpcException BatchIsEmpty() => new("The JSON-RPC batch should contain at least one item.");
}
