using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcListWalletsResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<WalletInfo>? Result { get; set; }
}
