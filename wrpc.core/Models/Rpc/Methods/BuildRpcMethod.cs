using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Build;

namespace WasabiRpc.Models.Results;

public class BuildRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public Build? Params { get; set; }
}