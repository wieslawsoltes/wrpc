using System.Text.Json.Serialization;

namespace WasabiCli.Models.Results;

public class RpcCancelTransactionResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}
