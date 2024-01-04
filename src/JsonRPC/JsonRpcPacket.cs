namespace Larine.JsonRPC;

/// <summary>
/// The structure incapsulates a single JSON-RPC object or a batch of them
/// </summary>
/// <typeparam name="T"></typeparam>
public struct JsonRpcPacket<T> where T : JsonRpcObject
{
	private readonly bool _isBatch;
	private readonly T? _item;
	private readonly T[]? _batch;

	/// <summary>
	/// Incapsulates a single JSON-RPC object in a packet
	/// </summary>
	/// <param name="item">A JSON-RPC object</param>
	public JsonRpcPacket(T item)
	{
		_isBatch = false;
		_item = item;
	}

	/// <summary>
	/// Incapsulates a JSON-RPC object batch in a packet
	/// </summary>
	/// <param name="batch">A batch or JSON-RPC objects</param>
	public JsonRpcPacket(T[] batch)
	{
		_isBatch = true;
		_batch = batch;
	}


	/// <summary>
	/// Gets a value indicating that the current <see cref="JsonRpcPacket{T}"/> object holds at least one item
	/// </summary>
	public bool IsEmpty => _item is null && _batch is null;

	private void VerifyIsNotEmpty()
	{
		if (IsEmpty)
			throw JsonRpcException.ThrowPacketIsEmpty();
	}


	/// <summary>
	/// Gets a value indicating that the current <see cref="JsonRpcPacket{T}"/> object holds an object batch
	/// </summary>
	public bool IsBatch
	{
		get
		{
			VerifyIsNotEmpty();
			return _isBatch;
		}
	}

	/// <summary>
	/// A single JSON-RPC object
	/// </summary>
	public T? Item
	{
		get
		{
			VerifyIsNotEmpty();
			return _item;
		}
	}

	/// <summary>
	/// A JSON-RPC object batch
	/// </summary>
	public T[]? Batch
	{
		get
		{
			VerifyIsNotEmpty();
			return _batch;
		}
	}

	/// <summary>
	/// Converts the incaplulated JSON-RPC object items to an array
	/// </summary>
	/// <returns></returns>
	public T[] ToArray()
	{
		VerifyIsNotEmpty();
		return _isBatch? _batch! : new T[] { _item! };
	}

	/// <summary>
	/// Incapsulates a single JSON-RPC object in a packet
	/// </summary>
	/// <param name="item">A JSON-RPC object</param>
	public static implicit operator JsonRpcPacket<T>(T item) => new(item);

	/// <summary>
	/// Incapsulates a JSON-RPC object batch in a packet
	/// </summary>
	/// <param name="batch">A batch or JSON-RPC objects</param>
	public static implicit operator JsonRpcPacket<T>(T[] batch) => new(batch);

	/// <summary>
	/// Returns an empty packet instance
	/// </summary>
	public static JsonRpcPacket<T> Empty { get; } = new();
}
