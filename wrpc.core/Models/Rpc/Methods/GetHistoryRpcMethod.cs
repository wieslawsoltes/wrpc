using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Methods;

public class GetHistoryRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}