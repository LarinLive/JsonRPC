using Json.Schema;
using System;

namespace Larine.JsonRPC.OpenRPC.Dispatcher;

public sealed class OpenRpcMethodDescriptor : IEquatable<OpenRpcMethodDescriptor>
{
	public OpenRpcMethodDescriptor(string name)
	{
		Name = name;
	}

	public string Name { get; init; }

	public string? Summary { get; init; }

	public string? Description { get; init; }

	public bool Deprecated { get; init; } = false;

	public JsonSchema? Params { get; init; }

	public JsonSchema? Result { get; init; }

	public bool Equals(OpenRpcMethodDescriptor? other)
	{
		if (ReferenceEquals(this, other))
			return true;
		else if (other is null)
			return false;
		else
			return Name.Equals(other.Name);
	}

	public override bool Equals(object? obj) => Equals(obj as OpenRpcMethodDescriptor);

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	public override string ToString()
	{
		return Name;
	}

	public static bool operator ==(OpenRpcMethodDescriptor? x, OpenRpcMethodDescriptor? y)
	{
		if (ReferenceEquals(x, y))
			return true;
		else if (x is null)
			return false;
		else
			return x.Equals(y);
	}

	public static bool operator !=(OpenRpcMethodDescriptor? x, OpenRpcMethodDescriptor? y) => !(x == y);
}
