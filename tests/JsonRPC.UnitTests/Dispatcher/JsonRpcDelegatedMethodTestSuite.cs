using LarinLive.JsonRPC.Dispatcher;
using System;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LarinLive.JsonRPC.UnitTests;

/// <summary>
/// Tests for the <see cref="JrpcDelegatedMethod"/> class
/// </summary>
public class JsonRpcDelegatedMethodTestSuite
{
	private static Task<JrpcResponse?> AddExecuteAsync(JrpcRequest request, CancellationToken ct) 
		=> Task.FromResult((JrpcResponse?)request.CreateResult(JsonValue.Create(42)));

	[Fact]
	public void SameInterfacesEqual()
	{
		// arrange
		var a = new JrpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		var b = new JrpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullInterfaceInequal()
	{
		// arrange
		var a = new JrpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		IEquatable<JrpcDelegatedMethod>? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentInterfacesInequal()
	{
		// arrange
		var a = new JrpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		var b = new JrpcDelegatedMethod("bbb", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentObjectsInequal()
	{
		// arrange
		var a = new JrpcDelegatedMethod("aaa", AddExecuteAsync);
		var b = new JrpcDelegatedMethod("bbb", AddExecuteAsync);
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void SameObjectEqual()
	{
		// arrange
		var a = new JrpcDelegatedMethod("aaa", AddExecuteAsync);
		var b = new JrpcDelegatedMethod("aaa", AddExecuteAsync);
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullObjectInequal()
	{
		// arrange
		object a = new JrpcDelegatedMethod("aaa", AddExecuteAsync);
		object ? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}
}
