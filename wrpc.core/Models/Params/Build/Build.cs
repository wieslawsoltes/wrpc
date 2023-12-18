using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Send;

namespace WasabiRpc.Models.Params.Build;

public class Build
{
    [JsonPropertyName("payments")]
    public List<Payment>? Payments { get; set; }

    [JsonPropertyName("coins")]
    public List<Coin>? Coins { get; set; }

    [JsonPropertyName("feeTarget")]
    public int? FeeTarget { get; set; }

    [JsonPropertyName("feeRate")]
    public decimal? FeeRate { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
