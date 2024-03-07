using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class PaymentInCoinjoinStateInfo
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("round")]
    public string? Round { get; set; }

    [JsonPropertyName("txid")]
    public string? TxId { get; set; }
}
