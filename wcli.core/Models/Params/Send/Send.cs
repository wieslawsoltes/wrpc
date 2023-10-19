using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.Params.Send;

public class Send
{
    [JsonPropertyName("payments")]
    public List<Payment>? Payments { get; set; }

    [JsonPropertyName("coins")]
    public List<Coin>? Coins { get; set; }

    [JsonPropertyName("feeTarget")]
    public int? FeeTarget { get; set; }

    [JsonPropertyName("feeRate")]
    public int? FeeRate { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
