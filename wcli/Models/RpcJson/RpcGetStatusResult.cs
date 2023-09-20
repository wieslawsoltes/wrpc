using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcGetStatusResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public StatusInfo? Result { get; set; }
}
