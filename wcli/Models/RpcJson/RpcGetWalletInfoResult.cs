using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcGetWalletInfoResult : Rpc
{
    [JsonPropertyName("result")]
    public WalletInfo? Result { get; set; }
}
