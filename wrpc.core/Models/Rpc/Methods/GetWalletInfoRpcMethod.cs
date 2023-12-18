using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Methods;

public class GetWalletInfoRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}