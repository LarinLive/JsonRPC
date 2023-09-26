using System.Text.Json.Nodes;

namespace Larine.JsonRPC;

public abstract class JsonRpcID
{
	protected internal abstract JsonValue ToJsonValue();
}
