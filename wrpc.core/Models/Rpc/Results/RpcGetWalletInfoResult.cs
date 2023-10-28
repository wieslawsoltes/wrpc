using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Results;

public class RpcGetWalletInfoResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public WalletInfo? Result { get; set; }
}
