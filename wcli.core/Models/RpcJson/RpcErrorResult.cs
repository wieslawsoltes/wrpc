using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcErrorResult : Rpc
{
    [JsonPropertyName("error")]
    [JsonRequired]
    public ErrorInfo? Error { get; set; }
}
