using Json.More;
using Json.Schema;
using System.Text.Json.Nodes;
using Xunit;

namespace LarinLive.JsonRPC.UnitTests;

/// <summary>
/// Tests for the <see cref="JrpcSchema"/> class
/// </summary>
public class JsonRpcSchemaTestSuite
{
	private static readonly EvaluationOptions _evaluationOptions = new() { OutputFormat = OutputFormat.Hierarchical };

	[Fact]
	public void RequestSchemaExists()
	{
		// arrange & act 
		var requestSchema = JrpcSchema.Request;
		// act
		Assert.NotNull(requestSchema);
	}

	[Fact]
	public void ResponseSchemaExists()
	{
		// arrange & act 
		var responseSchema = JrpcSchema.Response.ToJsonDocument();
		// act
		Assert.NotNull(responseSchema);
	}

	[Fact]
	public void ValidateRequestSchemaItself()
	{
		// arrange
		var requestSchema = JrpcSchema.Request.ToJsonDocument();
		// act & assert
		Assert.True(MetaSchemas.Draft202012.Evaluate(requestSchema, _evaluationOptions).IsValid);
	}

	[Fact]
	public void ValidateResponseSchemaItself()
	{
		// arrange
		var responseSchema = JrpcSchema.Response.ToJsonDocument();
		// act & assert
		Assert.True(MetaSchemas.Draft202012.Evaluate(responseSchema, _evaluationOptions).IsValid);
	}

	[Theory]
	[InlineData("""{"jsonrpc": "2.0", "method": "subtract", "params": [42, 23], "id": 1}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "subtract", "params": [42, 23], "id": null}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "subtract", "params": {"subtrahend": 23, "minuend": 42}, "id": 3}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "update", "params": [1,2,3,4,5]}""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "method": "sum", "params": [1,2,4], "id": "1"}
		]
	""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "method": "sum", "params": [1,2,4], "id": "1"},
			{"jsonrpc": "2.0", "method": "notify_hello", "params": [7] },
			{ "jsonrpc": "2.0", "method": "foo.get", "params": { "name": "myself"}, "id": "5"},
			{ "jsonrpc": "2.0", "method": "get_data", "id": "9"}
		]
	""")]
	public void ValidRequest(string input)
	{
		// arrange
		var request = JsonNode.Parse(input);
		// act
		var result = JrpcSchema.Request.Evaluate(request, _evaluationOptions);
		// assert
		Assert.True(result.IsValid);
	}


	[Theory]
	[InlineData("""{"jsonrpc": "2.0", "method": "update", "params": [1,2,3,4,5]}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "foobar"}""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "method": "sum", "params": [1,2,4]}
		]
	""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "method": "sum", "params": [1,2,4] },
			{"jsonrpc": "2.0", "method": "notify_hello", "params": [7] },
			{ "jsonrpc": "2.0", "method": "foo.get", "params": { "name": "myself"} },
			{ "jsonrpc": "2.0", "method": "get_data"}
		]
	""")]
	public void ValidNotification(string input)
	{
		// arrange
		var request = JsonNode.Parse(input);
		// act
		var result = JrpcSchema.Request.Evaluate(request, _evaluationOptions);
		// assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[InlineData("""{"jsonrpc": "2.0", "result": 19, "id": 1}""")]
	[InlineData("""{"jsonrpc": "2.0", "result": -19, "id": 2}""")]
	[InlineData("""{"jsonrpc": "2.0", "result": null, "id": 2}""")]
	[InlineData("""{"jsonrpc": "2.0", "result": -19, "id": null}""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "result": 7, "id": "1"}
		]
	""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "result": 7, "id": "1"},
			{"jsonrpc": "2.0", "result": 19, "id": "2"},
			{"jsonrpc": "2.0", "result": ["hello", 5], "id": "9"},
			{"jsonrpc": "2.0", "result": 7, "id": "1"}
		]
	""")]
	public void ValidResult(string input)
	{
		// arrange
		var response = JsonNode.Parse(input);
		// act
		var result = JrpcSchema.Response.Evaluate(response, _evaluationOptions);
		// assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[InlineData("""{"jsonrpc": "2.0", "error": {"code": -32600, "message": "Invalid Request"}, "id": null}""")]
	[InlineData("""{"jsonrpc": "2.0", "error": {"code": -32700, "message": "Parse error"}, "id": null}""")]
	[InlineData("""{"jsonrpc": "2.0", "error": {"code": -32601, "message": "Method not found"}, "id": "1"}""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "error": {"code": -32600, "message": "Invalid Request"}, "id": null},
			{"jsonrpc": "2.0", "result": 7, "id": "1"},
			{"jsonrpc": "2.0", "result": 7, "id": "1"},
			{"jsonrpc": "2.0", "error": {"code": -32601, "message": "Method not found"}, "id": "5"}
		]
		""")]
	public void ValidError(string input)
	{
		// arrange
		var response = JsonNode.Parse(input);
		// act
		var result = JrpcSchema.Response.Evaluate(response, _evaluationOptions);
		// assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[InlineData("""{}""")]
	[InlineData("""[]""")]
	[InlineData("""[1]""")]
	[InlineData("""[1,2,3]""")]
	[InlineData("""{"jsonrpc": "2.0", "methogd": "foobar", "params": "bar"}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": 1, "params": "bar"}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "test1", "id": []}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "test1", "id": { "property": null}}""")]
	[InlineData("""{"jsonrpc": "2.0", "method": "test1", "params": "bar", "extra": "2"}""")]
	[InlineData("""
		[
			{"jsonrpc": "2.0", "method": "subtract", "params": [42,23], "id": "2"},
			{"foo": "boo"}
		]
		""")]
	public void InvalidRequest(string input)
	{
		// arrange
		var request = JsonNode.Parse(input);
		// act
		var result = JrpcSchema.Request.Evaluate(request, _evaluationOptions);
		// assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[InlineData("""{}""")]
	[InlineData("""[]""")]
	[InlineData("""[1]""")]
	[InlineData("""[1,2,3]""")]
	[InlineData("""{"jsonrpc": "1.0"}""")]
	[InlineData("""{"jsonrpc": "2.0", "result": "bar"}""")]
	[InlineData("""{"jsonrpc": "2.0", "result": "bar", "extra": "foo"}""")]
	[InlineData("""{"jsonrpc": "2.0", "result": "bar", "error": "bar"}""")]
	[InlineData("""{"jsonrpc": "2.0", "error": {"code": -42601, "message": "Method not found", "extra": "foo"}, "id": "1"}""")]
	[InlineData("""{"jsonrpc": "2.0", "error": {"code": -42601, "message": "Method not found"}}""")]
	[InlineData("""{"jsonrpc": "2.0", "error": {"code": -42601, "message": "Method not found"}, "extra": "foo"}""")]
	[InlineData(""" 
		[
			{"jsonrpc": "2.0", "error": {"code": -32600, "message": "Invalid Request"}, "id": null},
			{"jsonrpc": "2.0", "resfult": 7, "id": "1"},
			{"jsonrpc": "2.0", "error": {"code": -32601, "message": "Method not found"}, "id": "5"}
		]
		""")]
	public void InvalidResponse(string input)
	{
		// arrange
		var response = JsonNode.Parse(input);
		// act
		var result = JrpcSchema.Response.Evaluate(response, _evaluationOptions);
		// assert
		Assert.False(result.IsValid);
	}
}
