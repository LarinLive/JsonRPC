using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using LarinLive.JsonRPC.Dispatcher;
using System.Linq;

namespace LarinLive.JsonRPC.AspNetCore;

public sealed class JrpcEndpoint
{
	private readonly IJrpcDispatcher _dispatcher;
	private readonly JrpcSerializer _jrpcSerializer;
	private readonly JsonWriterOptions _jrpcWriterOptions;

	public JrpcEndpoint(IJrpcDispatcher dispatcher, JsonWriterOptions jrpcWriterOptions, JrpcSerializer jrpcSerializer)
	{
		_dispatcher = dispatcher;
		_jrpcWriterOptions = jrpcWriterOptions;
		_jrpcSerializer = jrpcSerializer;
	}

	private static async Task SendTextResultAsync(HttpResponse httpResponse, int statusCode, string result, CancellationToken ct)
	{
		httpResponse.StatusCode = statusCode;
		httpResponse.ContentType = "text/plain; charset=utf-8";
		await httpResponse.StartAsync(ct);
		await httpResponse.WriteAsync(result, ct);
	}

	private async Task SendJrpcResultAsync(HttpResponse httpResponse, JrpcPacket<JrpcResponse> jrpcResponse, CancellationToken ct)
	{
		if (!jrpcResponse.IsEmpty)
		{
			httpResponse.StatusCode = StatusCodes.Status200OK;
		}
		else
		{
			httpResponse.ContentLength = 0;
			httpResponse.StatusCode = StatusCodes.Status204NoContent;
		}
		httpResponse.ContentType = "application/json; charset=utf-8";
		await httpResponse.StartAsync(ct);
		if (!jrpcResponse.IsEmpty)
		{
			await using var writer = new Utf8JsonWriter(httpResponse.BodyWriter, _jrpcWriterOptions);
			_jrpcSerializer.WriteTo(jrpcResponse, writer);
		}
	}

	public async Task HandleAsync(HttpContext context, CancellationToken ct = default)
	{
		var requestHeaders = context.Request.GetTypedHeaders();
		if (requestHeaders.ContentType?.MediaType.Value?.ToLowerInvariant() != "application/json")
		{
			await SendTextResultAsync(context.Response, StatusCodes.Status415UnsupportedMediaType, "The 'Content-Type' request header MUST be 'application/json'.", ct);
			return;
		}
		else if (requestHeaders.Accept.FirstOrDefault(a => a.MediaType.Value?.ToLowerInvariant() == "application/json") is null)
		{
			await SendTextResultAsync(context.Response, StatusCodes.Status406NotAcceptable, "The 'Accept' request header MUST be 'application/json'.", ct);
			return;
		}

		JrpcPacket<JrpcResponse> jrpcResponse;
		JsonNode? jsonRequest;
		try
		{
			jsonRequest = await JsonNode.ParseAsync(context.Request.Body, null, new JsonDocumentOptions() { CommentHandling = JsonCommentHandling.Skip }, ct);
		}
		catch (JsonException e)
		{
			jrpcResponse = new JrpcResponse(JrpcError.ParseError.WithException(e), new JrpcNullID());
			await SendJrpcResultAsync(context.Response, jrpcResponse, ct);
			return;
		}

		JrpcPacket<JrpcRequest> jrpcRequest;
		try
		{
			jrpcRequest = jsonRequest?.ToJrpcRequest() ?? throw new InvalidDataException("An empty JSON is prohibited for JSON-RPC.");
		}
		catch (JrpcException e)
		{
			jrpcResponse = new JrpcResponse(JrpcError.InvalidRequest.WithException(e), new JrpcNullID());
			await SendJrpcResultAsync(context.Response, jrpcResponse, ct);
			return;
		}

		jrpcResponse = await _dispatcher.ExecuteAsync(jrpcRequest, ct);
		await SendJrpcResultAsync(context.Response, jrpcResponse, ct);
	}
}
