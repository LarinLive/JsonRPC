namespace Larin.JsonRPC;

public abstract class JsonRpcObject
{
	public virtual JsonRpcObjectType Type => JsonRpcObjectType.Unknown;
}
