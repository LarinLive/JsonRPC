namespace Larine.JsonRPC;

public struct JsonRpcPacket<T> where T : JsonRpcObject
{
	private readonly bool _isBatch;
	private readonly T? _item;
	private readonly T[]? _batch;

	public JsonRpcPacket(T item)
	{
		_isBatch = false;
		_item = item;
	}

	public JsonRpcPacket(T[] batch)
	{
		_isBatch = true;
		_batch = batch;
	}

	public readonly bool IsEmpty => _item is null && _batch is null;

	private void VerifyIsNotEmpty()
	{
		if (IsEmpty)
			throw new JsonRpcException("The JSON-RPC packet is empty.");
	}

	public bool IsBatch
	{
		get
		{
			VerifyIsNotEmpty();
			return _isBatch;
		}
	}

	public T? Item
	{
		get
		{
			VerifyIsNotEmpty();
			return _item;
		}
	}

	public T[]? Batch
	{
		get
		{
			VerifyIsNotEmpty();
			return _batch;
		}
	}

	public T[] ToArray()
	{
		VerifyIsNotEmpty();
		return _isBatch? _batch! : new T[] { _item! };
	}

	public static implicit operator JsonRpcPacket<T>(T item) => new(item);

	public static implicit operator JsonRpcPacket<T>(T[] batch) => new(batch);

	public static JsonRpcPacket<T> Empty { get; } = new();
}
