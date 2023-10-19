using System.Text.Json.Serialization;

namespace WasabiCli.Models.Results;

public class RpcCreateWalletResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}
