using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.RpcJson;

public class RpcGetFeeRatesResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public Dictionary<int, int>? Result { get; set; }
}
