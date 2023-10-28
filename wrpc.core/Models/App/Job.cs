using System.Text.Json.Serialization;
using WasabiRpc.Models.Rpc;

namespace WasabiRpc.Models.App;

public class Job
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("rpcMethod")]
    public RpcMethod RpcMethod { get; set; }

    [JsonPropertyName("rpcServerUri")]
    public string RpcServerUri { get; set; }

    [JsonConstructor]
    public Job(string name, RpcMethod rpcMethod, string rpcServerUri)
    {
        Name = name;
        RpcMethod = rpcMethod;
        RpcServerUri = rpcServerUri;
    }
}
