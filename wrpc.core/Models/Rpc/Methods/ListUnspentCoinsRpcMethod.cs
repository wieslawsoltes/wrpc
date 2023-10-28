using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Results;

public class ListUnspentCoinsRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}