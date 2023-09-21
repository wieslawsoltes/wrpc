using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcListKeysResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<KeyInfo>? Result { get; set; }
}
