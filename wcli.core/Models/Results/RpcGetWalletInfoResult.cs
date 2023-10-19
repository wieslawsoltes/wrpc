using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcGetWalletInfoResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public WalletInfo? Result { get; set; }
}
