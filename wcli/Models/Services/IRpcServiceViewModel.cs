using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.Models.Services;

public interface IRpcServiceViewModel
{
    Task<object?> Send<T>(RpcMethod rpcMethod, string rpcServerUri, JsonTypeInfo<T> jsonTypeInfo) where T: class;

    string? ServerPrefix { get; set; }
}
