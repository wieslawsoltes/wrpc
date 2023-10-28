using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Results;

public class RecoverWalletRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public string?[]? Params { get; set; }
}