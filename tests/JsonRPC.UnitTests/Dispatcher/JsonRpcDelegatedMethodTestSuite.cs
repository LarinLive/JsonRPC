using LarinLive.JsonRPC.Dispatcher;
using System;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LarinLive.JsonRPC.UnitTests.Dispatcher;

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
		var a = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		var b = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullInterfaceInequal()
	{
		// arrange
		var a = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		IEquatable<JrpcMethodBase>? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentInterfacesInequal()
	{
		// arrange
		var a = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		var b = JrpcDelegatedMethod.Create("bbb", AddExecuteAsync) as IEquatable<JrpcMethodBase>;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void DifferentObjectsInequal()
	{
		// arrange
		var a = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync);
		var b = JrpcDelegatedMethod.Create("bbb", AddExecuteAsync);
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	[Fact]
	public void SameObjectEqual()
	{
		// arrange
		var a = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync);
		var b = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync);
		// act
		var result = a.Equals(b);
		// assert
		Assert.True(result);
	}

	[Fact]
	public void NullObjectInequal()
	{
		// arrange
		object a = JrpcDelegatedMethod.Create("aaa", AddExecuteAsync);
		object? b = null;
		// act
		var result = a.Equals(b);
		// assert
		Assert.False(result);
	}

	private Task<JrpcResponse?> JrprMethodWithoutAttribute(JrpcRequest request, CancellationToken ct)
	{
		return Task.FromResult<JrpcResponse?>(null);
	}

	[Fact]
	public void JrprMethodAttributeAbsent()
	{
		// arrange
		// act
		// assert
		Assert.Throws<ArgumentException>(() => JrpcDelegatedMethod.Create(JrprMethodWithoutAttribute));
	}

	[JrpcMethodAttribule]
	private Task<JrpcResponse?> JrprMethodWithoutName(JrpcRequest request, CancellationToken ct)
	{
		return Task.FromResult<JrpcResponse?>(null);
	}

	[Fact]
	public void JrprMethodAttributeWithEmtpyName()
	{
		// arrange
		var expectedMethodName = $"{GetType().Name}.{nameof(JrprMethodWithoutName)}";
		// act
		var method = JrpcDelegatedMethod.Create(JrprMethodWithoutName);
		// assert
		Assert.Equal(expectedMethodName, method.Name);
	}

	private const string _testMethodName = "Test.Method";
	[JrpcMethodAttribule(_testMethodName)]
	private Task<JrpcResponse?> JrprMethodWithName(JrpcRequest request, CancellationToken ct)
	{
		return Task.FromResult<JrpcResponse?>(null);
	}

	[Fact]
	public void JrprMethodAttributeWithName()
	{
		// arrange
		// act
		var method = JrpcDelegatedMethod.Create(JrprMethodWithName);
		// assert
		Assert.Equal(_testMethodName, method.Name);
	}
}
