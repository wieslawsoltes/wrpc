using System.Threading.Tasks;
using WasabiRpc.Models.Results;

namespace WasabiRpc.Models.Services;

public interface IRpcServiceViewModel
{
    Task<object?> Send<TResult>(RpcMethod rpcMethod, string rpcServerUri, INavigationService navigationService) where TResult: class;

    Task<object?> Send<TResult>(RpcMethod[] rpcMethods, string rpcServerUri, INavigationService navigationService) where TResult: class;

    string? ServerPrefix { get; set; }

    bool BatchMode { get; set; }
}
