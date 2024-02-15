namespace Larin.JsonRPC;

/// <summary>
/// The structure incapsulates a single JSON-RPC object or a batch of them
/// </summary>
/// <typeparam name="T">The type of a packet payload</typeparam>
public readonly record struct JrpcPacket<T> where T : struct, IJrpcObject
{
	private readonly bool _isBatch;
	private readonly bool _isNotEmpty;
	private readonly T _item;
	private readonly T[]? _batch;

	/// <summary>
	/// Incapsulates an empty JSON-RPC object packet
	/// </summary>
	public JrpcPacket()
	{
	}

	/// <summary>
	/// Incapsulates a single JSON-RPC object in a packet
	/// </summary>
	/// <param name="item">A JSON-RPC object</param>
	public JrpcPacket(T item)
	{
		_isNotEmpty = true;
		_isBatch = false;
		_item = item;
	}

	/// <summary>
	/// Incapsulates a JSON-RPC object batch in a packet
	/// </summary>
	/// <param name="batch">A batch or JSON-RPC objects</param>
	public JrpcPacket(T[] batch)
	{
		if (batch.Length == 0)
			throw JrpcException.BatchIsEmpty();
		_isNotEmpty = true;
		_isBatch = true;
		_batch = batch;
	}

	/// <summary>
	/// Gets a value indicating that the current <see cref="JrpcPacket{T}"/> object holds at least one item
	/// </summary>
	public bool IsEmpty => !_isNotEmpty;

	private void VerifyIsNotEmpty()
	{
		if (!_isNotEmpty)
			JrpcException.PacketIsEmpty();
	}


	/// <summary>
	/// Gets a value indicating that the current <see cref="JrpcPacket{T}"/> object holds an object batch
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
	public T Item
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
	public T[] Batch
	{
		get
		{
			VerifyIsNotEmpty();
			return _batch!;
		}
	}

	/// <summary>
	/// Converts the incapsulated JSON-RPC object items to an array
	/// </summary>
	/// <returns></returns>
	public T[] ToArray()
	{
		VerifyIsNotEmpty();
		return _isBatch ? _batch! : [_item];
	}

	/// <summary>
	/// Incapsulates a single JSON-RPC object in a packet
	/// </summary>
	/// <param name="item">A JSON-RPC object</param>
	public static implicit operator JrpcPacket<T>(T item) => new(item);

	/// <summary>
	/// Incapsulates a JSON-RPC object batch in a packet
	/// </summary>
	/// <param name="batch">A batch or JSON-RPC objects</param>
	public static implicit operator JrpcPacket<T>(T[] batch) => new(batch);

	/// <summary>
	/// Returns an empty packet instance
	/// </summary>
	public static JrpcPacket<T> Empty { get; } = new();
}
