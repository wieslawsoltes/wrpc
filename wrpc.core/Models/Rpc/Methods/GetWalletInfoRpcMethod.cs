using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Results;

public class GetWalletInfoRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public object? Params { get; set; }
}