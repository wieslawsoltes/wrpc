using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Methods;

public class ListUnspentCoinsRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}