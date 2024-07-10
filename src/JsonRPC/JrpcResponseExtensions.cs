using System.Threading.Tasks;

namespace LarinLive.JsonRPC;

/// <summary>
/// Extension methods for the <see cref="JrpcResponse"/> class.
/// </summary>
public static class JrpcResponseExtensions
{
	/// <summary>
	/// Produces a <see cref="Task{T}"/> result from a given JSON-RPC response.
	/// </summary>
	/// <param name="response">The JSON-RPC response to produce a task result.</param>
	/// <returns>A completed  <see cref="Task"/> object.</returns>
	public static Task<JrpcResponse> AsTaskResult(this JrpcResponse response)
		=> Task.FromResult(response);

	/// <summary>
	/// Produces a nullable <see cref="Task{T}"/> result from a given JSON-RPC response.
	/// </summary>
	/// <param name="response">The JSON-RPC response to produce a task result.</param>
	/// <returns>A completed  <see cref="Task"/> object.</returns>
	public static Task<JrpcResponse?> AsTaskResultNA(this JrpcResponse response)
		=> Task.FromResult((JrpcResponse?)response);
}
