using System.Threading;
using System.Threading.Tasks;

namespace Larine.JsonRPC;

public interface IJsonRpcDispatcher
{
	Task<JsonRpcPacket<JsonRpcResponse>> DispatchAsync(JsonRpcPacket<JsonRpcRequest> request, CancellationToken ct = default);
}
 