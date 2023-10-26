using System.Threading.Tasks;
using WasabiRpc.Models.Results;

namespace WasabiRpc.Models.Services;

public interface IRpcServiceViewModel
{
    Task<object?> Send<TResult>(RpcMethod rpcMethod, string rpcServerUri) where TResult: class;

    Task<object?> Send(RpcMethod[] rpcMethods, string rpcServerUri);

    string? ServerPrefix { get; set; }

    bool BatchMode { get; set; }
}
