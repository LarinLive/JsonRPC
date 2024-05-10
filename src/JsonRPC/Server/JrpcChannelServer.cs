using LarinLive.JsonRPC.Dispatcher;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LarinLive.JsonRPC.Server;

public class JrpcChannelServer
{
	private readonly IJrpcDispatcher _dispatcher;
	private readonly ChannelReader<JrpcPacket<JrpcRequest>> _in;
	private readonly ChannelWriter<JrpcPacket<JrpcResponse>> _out;

	public JrpcChannelServer(IJrpcDispatcher dispatcher, ChannelReader<JrpcPacket<JrpcRequest>> @in, ChannelWriter<JrpcPacket<JrpcResponse>> @out)
	{
		_dispatcher = dispatcher;
		_in = @in;
		_out = @out;
	}

	/// <summary>
	/// Run the JSON-RPC server asynchronously.
	/// </summary>
	/// <param name="ct">An optional cancellation token.</param>
	/// <returns>A <see cref="Task"/> that represents an asynchronous server runner.</returns>
	public async Task RunAsync(CancellationToken ct = default)
	{
		while (true)
		{
			ct.ThrowIfCancellationRequested();
			var request = await _in.ReadAsync(ct);
			var response = await _dispatcher.ExecuteAsync(request, ct);
			if (!response.IsEmpty)
				await _out.WriteAsync(response, ct);
		}
	}
}
