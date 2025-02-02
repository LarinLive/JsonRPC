using LarinLive.JsonRPC.Dispatcher;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading;

namespace LarinLive.JsonRPC.AspNetCore;

public static class JrpcEndpointRouteBuilderExtensions
{
	public static IEndpointConventionBuilder MapJrpc<T>(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern, T dispatcher,
		JsonWriterOptions jsonWriterOptions, JrpcExceptionSerializerOptions jrpcExceptionSerializerOptions) where T : IJrpcDispatcher
	{
		var endpoint = new JrpcEndpoint(dispatcher, jsonWriterOptions, new JrpcSerializer(jrpcExceptionSerializerOptions));
		return endpoints.MapPost(pattern, async (HttpContext context, CancellationToken ct) => { await endpoint.HandleAsync(context, ct); }).WithDisplayName("JsonRPC Endpoint");
	}
}