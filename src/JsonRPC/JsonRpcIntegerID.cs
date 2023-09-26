using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

public sealed class JsonRpcIntegerID : JsonRpcID
{
	public JsonRpcIntegerID(long id)
	{
		ID = id;
	}

	public long ID { get; } 

	protected internal override JsonValue ToJsonValue()
	{
		return (JsonValue)ID;
	}

	public override string ToString()
	{
		return ID.ToString();
	}
}
