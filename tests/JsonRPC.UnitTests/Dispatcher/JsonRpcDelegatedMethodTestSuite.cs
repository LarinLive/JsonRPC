using LarinLive.JsonRPC.Dispatcher;
using System.Text.Json.Nodes;
using Xunit;

namespace LarinLive.JsonRPC.UnitTests;

/// <summary>
/// Tests for the <see cref="JsonRpcDelegatedMethod"/> class
/// </summary>
public class JsonRpcDelegatedMethodTestSuite
{
	private static Task<JrpcResponse?> AddExecuteAsync(JrpcRequest request, CancellationToken ct) 
		=> Task.FromResult((JrpcResponse?)request.CreateResult(JsonValue.Create(42)));

	[Fact]
	public void SameInterfacesEqual()
	{
		// arrange
		var a = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JsonRpcMethodBase>;
		var b = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JsonRpcMethodBase>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullInterfaceInequal()
	{
		// arrange
		var a = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JsonRpcMethodBase>;
		IEquatable<JsonRpcDelegatedMethod>? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentInterfacesInequal()
	{
		// arrange
		var a = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync) as IEquatable<JsonRpcMethodBase>;
		var b = new JsonRpcDelegatedMethod("bbb", AddExecuteAsync) as IEquatable<JsonRpcMethodBase>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentObjectsInequal()
	{
		// arrange
		var a = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync);
		var b = new JsonRpcDelegatedMethod("bbb", AddExecuteAsync);
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void SameObjectEqual()
	{
		// arrange
		var a = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync);
		var b = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync);
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullObjectInequal()
	{
		// arrange
		object a = new JsonRpcDelegatedMethod("aaa", AddExecuteAsync);
		object ? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}
}
