using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.Results;

public class RpcGetFeeRatesResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public Dictionary<int, int>? Result { get; set; }
}
