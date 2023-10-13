using System.Text.Json.Serialization;

namespace WasabiCli.Models.RpcJson;

public class RpcCancelTransactionResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}