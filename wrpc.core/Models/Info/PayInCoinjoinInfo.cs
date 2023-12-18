using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class PayInCoinjoinInfo
{
    [JsonPropertyName("paymentId")]
    public string? PaymentId { get; set; }
}
