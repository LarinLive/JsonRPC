using Json.Schema;
using Larine.JsonRPC.Dispatcher;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;

namespace Larine.JsonRPC.UnitTests;

/// <summary>
/// Tests for the <see cref="JsonRpcDispatcher"/> class
/// </summary>
public class JsonRpcDispatcherTestSuite
{
	private static JsonRpcDelegatedMethod AddMethod()
		=> new("add",
				(request, ct) =>
				{
					var args = JsonSerializer.Deserialize<decimal[]>(request.Params)!;
					decimal? sum = null;
					for (var i = 0; i < args.Length; i++)
						sum = (sum ?? 0M) + args[i];
					var response = request.CreateResult(JsonValue.Create(sum)!);
					return ValueTask.FromResult(response)!;
				}
			)
		{ Params = JsonSchema.FromText("""
{
	"$schema": "https://json-schema.org/draft/2020-12/schema",
	"type": "array",
	"minItems" : 1,
	"items": {
		"type": "number"
	}
}
""") };


	[Fact]
	public void ExecuteSingleRequest()
	{
		// arrange
		var id = Guid.NewGuid().ToString();
		var dispatcher = new JsonRpcDispatcher(new[] { AddMethod() });
		var request = new JsonRpcRequest("add", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), new JsonRpcID<string>(id));
		
		// act
		var result = dispatcher.ExecuteAsync(request, CancellationToken.None)?.Result;
		
		// assert
		Assert.NotNull(result);
		Assert.False(result.Value.IsEmpty);
		Assert.False(result.Value.IsBatch);
		var singleResult = result.Value.Item!;
		Assert.IsType<JsonRpcID<string>>(singleResult.ID);
		Assert.Equal(id, ((JsonRpcID<string>)singleResult.ID).Value);
		Assert.True(singleResult.IsSuccess);
		Assert.NotNull(singleResult.Result);
		Assert.Null(singleResult.Error);
		Assert.IsAssignableFrom<JsonValue?>(singleResult.Result);
		Assert.Equal(6M, JsonSerializer.Deserialize<decimal>(singleResult.Result));
	}

	[Fact]
	public void ExecuteBatchRequest()
	{
		// arrange
		var dispatcher = new JsonRpcDispatcher(new[] { AddMethod() });
		var request = new[]
		{
			new JsonRpcRequest("add", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), new JsonRpcID<long>(1)),
			new JsonRpcRequest("add", new JsonArray(JsonValue.Create(2), JsonValue.Create(3)), new JsonRpcID<long>(2))
		};

		// act
		var result = dispatcher.ExecuteAsync(request, CancellationToken.None)?.Result;

		// assert
		Assert.NotNull(result);
		Assert.False(result.Value.IsEmpty);
		Assert.True(result.Value.IsBatch);
		var batchResult = result.Value.Batch!;
		Assert.Equal(request.Length, batchResult.Length);

		var firstResult = batchResult.Single(i => i.ID is JsonRpcID<long> lid && lid.Value == 1);
		Assert.True(firstResult.IsSuccess);
		Assert.NotNull(firstResult.Result);
		Assert.Null(firstResult.Error);
		Assert.IsAssignableFrom<JsonValue?>(firstResult.Result);
		Assert.Equal(6M, JsonSerializer.Deserialize<decimal>(firstResult.Result));

		var secondResult = batchResult.Single(i => i.ID is JsonRpcID<long> lid && lid.Value == 2);
		Assert.True(secondResult.IsSuccess);
		Assert.NotNull(secondResult.Result);
		Assert.Null(secondResult.Error);
		Assert.IsAssignableFrom<JsonValue?>(secondResult.Result);
		Assert.Equal(5M, JsonSerializer.Deserialize<decimal>(secondResult.Result));
	}

	[Fact]
	public void ExecuteSingleNotification()
	{
		// arrange
		var dispatcher = new JsonRpcDispatcher(new[] { AddMethod() });
		var request = new JsonRpcRequest("add", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), null);
		// act
		var result = dispatcher.ExecuteAsync(request, CancellationToken.None)?.Result;
		// assert
		Assert.NotNull(result);
		Assert.True(result.Value.IsEmpty);
	}

	[Fact]
	public void ExecuteBatchNotification()
	{
		// arrange
		var dispatcher = new JsonRpcDispatcher(new[] { AddMethod() });
		var request = new[]
		{
			new JsonRpcRequest("add", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), null),
			new JsonRpcRequest("add", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), null)
		};
		// act
		var result = dispatcher.ExecuteAsync(request, CancellationToken.None)?.Result;
		// assert
		Assert.NotNull(result);
		Assert.False(result.Value.IsEmpty);
		Assert.True(result.Value.IsBatch);
		var batchResult = result.Value.Batch!;
		Assert.Empty(batchResult);
	}


	[Fact]
	public void ExecuteSingleWrongMethod()
	{
		// arrange
		var id = Guid.NewGuid().ToString();
		var dispatcher = new JsonRpcDispatcher(new[] { AddMethod() });
		var request = new JsonRpcRequest("adds", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), new JsonRpcID<string>(id));
		// act
		var result = dispatcher.ExecuteAsync(request, CancellationToken.None)?.Result;
		// assert
		Assert.NotNull(result);
		Assert.False(result.Value.IsEmpty);
		Assert.False(result.Value.IsBatch);
		var singleResult = result.Value.Item!;
		Assert.IsType<JsonRpcID<string>>(singleResult.ID);
		Assert.Equal(id, ((JsonRpcID<string>)singleResult.ID).Value);
		Assert.False(singleResult.IsSuccess);
		Assert.Null(singleResult.Result);
		Assert.NotNull(singleResult.Error);
		Assert.Equal(JsonRpcError.MethodNotFound.Code, singleResult.Error.Code);
	}
}
