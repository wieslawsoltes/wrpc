using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcListWalletsResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<WalletInfo>? Result { get; set; }
}
