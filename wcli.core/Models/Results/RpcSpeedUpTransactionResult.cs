using System.Text.Json.Serialization;

namespace WasabiCli.Models.Results;

public class RpcSpeedUpTransactionResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}
