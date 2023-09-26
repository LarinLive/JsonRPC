namespace Larine.JsonRPC;

public abstract class JsonRpcObject
{
	public virtual JsonRpcObjectType Type => JsonRpcObjectType.Unknown;
}
