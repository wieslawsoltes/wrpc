using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcCancelTransactionResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}
