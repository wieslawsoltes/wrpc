using System.Text.Json.Serialization;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.Models.App;

public class Job
{
    [JsonPropertyName("rpcMethod")]
    public RpcMethod RpcMethod { get; set; }

    [JsonPropertyName("rpcServerUri")]
    public string RpcServerUri { get; set; }

    public Job(RpcMethod rpcMethod, string rpcServerUri)
    {
        RpcMethod = rpcMethod;
        RpcServerUri = rpcServerUri;
    }
}
