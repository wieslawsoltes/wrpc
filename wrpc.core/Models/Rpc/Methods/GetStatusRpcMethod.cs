using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Results;

public class GetStatusRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}