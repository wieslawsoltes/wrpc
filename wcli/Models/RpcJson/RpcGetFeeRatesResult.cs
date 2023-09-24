using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcGetFeeRatesResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public Dictionary<int, int>? Result { get; set; }
}
