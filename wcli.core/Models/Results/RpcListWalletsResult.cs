using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcListWalletsResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<WalletInfo>? Result { get; set; }
}
