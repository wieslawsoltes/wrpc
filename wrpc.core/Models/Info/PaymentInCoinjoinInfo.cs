using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class PaymentInCoinjoinInfo
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("destination")]
    public string? Destination { get; set; }

    [JsonPropertyName("state")]
    public PaymentInCoinjoinStateInfo? State { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }
}
