using Json.Schema;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace LarinLive.JsonRPC.Dispatcher;


/// <summary>
/// A base class for JSON-RPC methods.
/// </summary>
public abstract class JsonRpcMethodBase : IEquatable<JsonRpcMethodBase>
{
	/// <summary>
	/// Creates a new instance of the <see cref="JsonRpcMethodBase"/> class.
	/// </summary>
	/// <param name="name">A method name</param>
	public JsonRpcMethodBase(string name)
	{
		Name = name;
	}

	/// <summary>
	/// The method name,
	/// </summary>
	public string Name { get; init; }

	/// <summary>
	/// The schema of the method parameters,
	/// </summary>
	public JsonSchema? Params { get; init; }

	/// <summary>
	/// The schema of the method result,
	/// </summary>
	public JsonSchema? Result { get; init; }

	/// <inheritdoc/>
	public bool Equals(JsonRpcMethodBase? other)
	{
		if (ReferenceEquals(this, other))
			return true;
		else if (other is null)
			return false;
		else
			return Name.Equals(other.Name);
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj) => Equals(obj as JsonRpcMethodBase);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return Name;
	}

	public static bool operator ==(JsonRpcMethodBase? x, JsonRpcMethodBase? y)
	{
		if (ReferenceEquals(x, y))
			return true;
		else if (x is null)
			return false;
		else
			return x.Equals(y);
	}

	public static bool operator !=(JsonRpcMethodBase? x, JsonRpcMethodBase? y) => !(x == y);

	/// <summary>
	/// Executes the JSON-RPC method asynchronously.
	/// </summary>
	/// <param name="request">A request to be executed.</param>
	/// <param name="ct">A cancellation token</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public abstract Task<JrpcResponse?> ExecuteAsync(JrpcRequest request, CancellationToken ct);
}