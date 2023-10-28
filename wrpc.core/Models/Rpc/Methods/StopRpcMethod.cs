using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Results;

public class StopRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}