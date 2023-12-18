using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class GetFeeRatesInfo
{
    [JsonPropertyName("feeRates")]
    public Dictionary<int, int>? FeeRates { get; set; }
}
