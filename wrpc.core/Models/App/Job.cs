using System;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Results;

namespace WasabiRpc.Models.App;

public class Job
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("rpcMethod")]
    public RpcMethod RpcMethod { get; set; }

    [JsonPropertyName("rpcServerUri")]
    public string RpcServerUri { get; set; }

    [JsonPropertyName("resultType")]
    public string ResultType { get; set; }

    [JsonConstructor]
    public Job(string name, RpcMethod rpcMethod, string rpcServerUri, Type resultType)
    {
        Name = name;
        RpcMethod = rpcMethod;
        RpcServerUri = rpcServerUri;
        ResultType = resultType.ToString();
    }
}
