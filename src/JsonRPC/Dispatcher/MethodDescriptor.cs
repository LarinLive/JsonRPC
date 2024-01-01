using Json.Schema;
using System;

namespace Larine.JsonRPC.Dispatcher;


/// <summary>
/// A JSON-RPC method descriptor for using with <see cref="DelegatedDispatcher"/> class
/// </summary>
public sealed class MethodDescriptor : IEquatable<MethodDescriptor>
{
	/// <summary>
	/// Creates a new instance of the <see cref="MethodDescriptor"/> class
	/// </summary>
	/// <param name="name">A method name</param>
	public MethodDescriptor(string name)
	{
		Name = name;
	}

	/// <summary>
	/// The method name
	/// </summary>
	public string Name { get; init; }

	/// <summary>
	/// The schema of the method parameters
	/// </summary>
	public JsonSchema? Params { get; init; }

	/// <summary>
	/// The schema of the method result
	/// </summary>
	public JsonSchema? Result { get; init; }

	/// <inheritdoc/>
	public bool Equals(MethodDescriptor? other)
	{
		if (ReferenceEquals(this, other))
			return true;
		else if (other is null)
			return false;
		else
			return Name.Equals(other.Name);
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj) => Equals(obj as MethodDescriptor);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return Name;
	}

	/// <inheritdoc/>
	public static bool operator ==(MethodDescriptor? x, MethodDescriptor? y)
	{
		if (ReferenceEquals(x, y))
			return true;
		else if (x is null)
			return false;
		else
			return x.Equals(y);
	}

	/// <inheritdoc/>
	public static bool operator !=(MethodDescriptor? x, MethodDescriptor? y) => !(x == y);
}
