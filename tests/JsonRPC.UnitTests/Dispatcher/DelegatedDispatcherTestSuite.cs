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
		=> new JsonRpcDelegatedMethod("add",
				(request, ct) =>
				{
					var args = JsonSerializer.Deserialize<decimal[]>(request.Params)!;
					decimal? sum = null;
					for (var i = 0; i < args.Length; i++)
						sum = (sum ?? 0M) + args[i];
					var response = request.CreateResult(JsonValue.Create(sum));
					return ValueTask.FromResult(response);
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
	public void ExecuteSingle()
	{
		// arrange
		var id = Guid.NewGuid().ToString();
		var dispatcher = new JsonRpcDispatcher(new[] { AddMethod() });
		var request = new JsonRpcRequest("add", new JsonArray(JsonValue.Create(1), JsonValue.Create(2), JsonValue.Create(3)), new JsonRpcStringID(id));
		// act
		var result = dispatcher.ExecuteAsync(request, CancellationToken.None)?.Result;
		// assert
		Assert.NotNull(result);
		Assert.False(result.Value.IsEmpty);
		Assert.False(result.Value.IsBatch);
		var singleResult = result.Value.Item!;
		Assert.True(singleResult.IsSuccess);
		Assert.IsType<JsonRpcStringID>(singleResult.ID);
		Assert.NotNull(singleResult.Result);
		Assert.IsAssignableFrom<JsonValue?>(singleResult.Result);
		Assert.Equal(6M, JsonSerializer.Deserialize<decimal>(singleResult.Result));
	}
}
