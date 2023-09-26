using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

public sealed class JsonRpcStringID : JsonRpcID
{
	public JsonRpcStringID(string id)
	{
		ID = id;
	}

	public string ID { get; } 

	protected internal override JsonValue ToJsonValue()
	{
		return JsonValue.Create(ID)!;
	}

	public override string ToString()
	{
		return ID.ToString();
	}
}
