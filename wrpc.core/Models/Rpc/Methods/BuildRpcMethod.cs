using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Build;

namespace WasabiRpc.Models.Rpc.Methods;

public class BuildRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public Build? Params { get; set; }
}