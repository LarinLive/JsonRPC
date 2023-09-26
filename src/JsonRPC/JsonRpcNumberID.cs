using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

public sealed class JsonRpcNumberID : JsonRpcID
{
	public JsonRpcNumberID(double id)
	{
		ID = id;
	}

	public double ID { get; } 

	protected internal override JsonValue ToJsonValue()
	{
		return (JsonValue)ID;
	}

	public override string ToString()
	{
		return ID.ToString();
	}
}
