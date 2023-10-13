using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi.Send;

namespace WasabiCli.Models.WalletWasabi.Build;

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
