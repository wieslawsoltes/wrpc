using System.Text.Json.Serialization;

namespace wcli.Models;

public class RpcGetWalletInfoResult : Rpc
{
    [JsonPropertyName("result")]
    public GetWalletInfoResult? Result { get; set; }
}
