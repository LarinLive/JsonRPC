using Larine.JsonRPC.Dispatcher;
using Xunit;

namespace Larine.JsonRPC.UnitTests;

/// <summary>
/// Tests for the <see cref="DelegatedDispatcher"/> class
/// </summary>
public class DelegatedDispatcherTestSuite
{
	private async ValueTask<Jso> 

	[Fact]
	public void ExecuteSingle()
	{
		// arrange
		var  = new MethodDescriptor("aaa") as IEquatable<MethodDescriptor>;
		var b = new MethodDescriptor("aaa") as IEquatable<MethodDescriptor>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullInequal()
	{
		// arrange
		var a = new MethodDescriptor("aaa") as IEquatable<MethodDescriptor>;
		IEquatable<MethodDescriptor>? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentInequal()
	{
		// arrange
		var a = new MethodDescriptor("aaa") as IEquatable<MethodDescriptor>;
		var b = new MethodDescriptor("bbb") as IEquatable<MethodDescriptor>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void SameObjectEqual()
	{
		// arrange
		var a = new MethodDescriptor("aaa");
		var b = new MethodDescriptor("aaa");
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullObjectInequal()
	{
		// arrange
		object a = new MethodDescriptor("aaa");
		object ? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}
}
